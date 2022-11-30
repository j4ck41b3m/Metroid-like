using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPlayer : MonoBehaviour
{
    public int velocidad;
    public Rigidbody2D jugador;
    public SpriteRenderer spritee;
    private int fuerzaSalto;
    private Animator animator;
    public int puntuacion;
    public int vidas;
    public bool vulnerable, shottan, up;
    public int numeroPowerUps;

    public int tiempoNivel;
    private float tiempoInicio, intervaloBala;
    private int tiempoEmpleado;
    
    public Canvas canvas;
    private ControlHud hud;

    public AudioClip sonidoPower;
    public AudioClip sonidoVida;

    private AudioSource audio;
    public ControlDatosJuego datosJuego;

    public LayerMask Ground;

    public GameObject blaster, upblaster, bullet, upbullet;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GetComponent<Rigidbody2D>();
        spritee = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        velocidad = 10;
        fuerzaSalto = 10;

        vulnerable = true;

        tiempoInicio = Time.time;

        hud = canvas.GetComponent<ControlHud>();
        hud.SetPower(numeroPowerUps);
        hud.SetVidas(vidas);

        datosJuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();


    }

    // Update is called once per frame
    void Update()
    {
        if (jugador.velocity.x > 0)
        {
            spritee.flipX = false;
            blaster.transform.position = new Vector3(transform.position.x + 1.188f, blaster.transform.position.y, blaster.transform.position.z);

        }
        else if (jugador.velocity.x < 0)
        {
            spritee.flipX = true;
            blaster.transform.position = new Vector3(transform.position.x - 1.188f, blaster.transform.position.y, blaster.transform.position.z);


        }
        Debug.Log(TocandoSuelo());
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo() == true)
        {
            jugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
        AnimarJugador();
        //Debug.Log(jugador.velocity.x);
        //Debug.Log(Input.GetAxis("Horizontal"));

        tiempoEmpleado = (int)Time.time - (int)tiempoInicio;
        if (tiempoNivel-tiempoEmpleado <0)
        {
            FinJuego();
            Ganado();
        }

        hud.SetTiempo(tiempoEmpleado);
        // hud.SetTiempo(Time.deltaTime);

        if (Input.GetKey(KeyCode.W))
        {
            up = true;
           
        }
        else up = false;

            if (Input.GetKey(KeyCode.Mouse0))
        {
            
            

            if (jugador.velocity.x != 0)
            {
                animator.Play("runNshoot");
                shottan = true;
                intervaloBala += Time.deltaTime;
                if (intervaloBala >= 0.2f)
                {
                    Instantiate(bullet, blaster.transform.position, Quaternion.identity);
                    intervaloBala = 0;
                }

            }
            else if (jugador.velocity.x == 0)
            {
                if (up == true)
                {
                    if (intervaloBala >= 0.2f)
                    {
                        Instantiate(upbullet, upblaster.transform.position, Quaternion.identity);
                        intervaloBala = 0;
                    }
                    up = true;
                }
                else
                {
                    animator.Play("shoot");

                    up = false; 
                }
            }

        }
        else
        {
            intervaloBala = 0;
            shottan = false;
        }
        //Debug.Log(intervaloBala);

    }

    public void Shoot()
    {
        shottan = true;
        Instantiate(bullet, blaster.transform.position, Quaternion.identity);
        
    }

    private void Ganado()
    {
        datosJuego.Ganado = true;
        SceneManager.LoadScene("Ganados");
    }

    private void Perdido()
    {
        datosJuego.Ganado = false;
        SceneManager.LoadScene("Menu");
    }

    public void IncrementarPuntuacion(int puntos)
    {
        puntuacion += puntos;
    }

    public void DecrementarPowerUps()
    {
        numeroPowerUps--;
        hud.SetPower(numeroPowerUps);
    }

    private void AnimarJugador()
    {
        if (TocandoSuelo() == false)
        {

            animator.Play("onAir");

        }
        else
           if (jugador.velocity.x == 0)
        {
            if (shottan != true)
            {
                if (up != true)
                {
                    animator.Play("Iddle");

                }
                else animator.Play("shootUP");

                animator.SetBool("aire", false);
                shottan = false;
            }
           

        }
        else
            if (shottan != true)
        {
            animator.Play("run");
            shottan = false;
        }
        /*else
             if (jugador.velocity.x > 1 || jugador.velocity.x <= -1 && jugador.velocity.y == 0)
            animator.Play("run");
        else
         if (jugador.velocity.x < 1 || jugador.velocity.x > -1 && jugador.velocity.y == 0)
            animator.Play("Iddle");*/
    }

    private void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");

        jugador.velocity = new Vector2(entradaX * velocidad, jugador.velocity.y);

        

    }

    private void Awake()
    {
        audio = GetComponent<AudioSource>();
    }
    public bool TocandoSuelo()
    {

        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0, -1.6f, 0)
            , Vector2.down, 1f, Ground);
        return toca.collider != null;

    }

    private void HacerVulnerable()
    {
        vulnerable = true;
        spritee.color = Color.white;
    }

    public void QuitarVidas()
    {
        if (vulnerable)
        {
            vulnerable = false;
            if (--vidas == 0)
            {
                FinJuego();
            }
            hud.SetVidas(vidas);
            spritee.color = Color.red;
            Invoke("HacerVulnerable", 1f);
        }
        
        
    }
    public void FinJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
