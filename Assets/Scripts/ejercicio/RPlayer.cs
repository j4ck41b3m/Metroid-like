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
    public bool vulnerable, shottan, up, charged, runnin;
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

    public GameObject blaster, upblaster, bullet, upbullet, super;
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
        //animator.Play("chargeUP");

        if (numeroPowerUps > 0)
        {
            charged = true;
        }
        else if (numeroPowerUps <= 0)
        {
            charged = false;
        }
            //Debug.Log(intervaloBala);
            if (jugador.velocity.x > 0)
        {
            spritee.flipX = false;
            blaster.transform.position = new Vector3(transform.position.x + 1.188f, blaster.transform.position.y, blaster.transform.position.z);
            upblaster.transform.position = new Vector3(transform.position.x + 0.25f, upblaster.transform.position.y, upblaster.transform.position.z);

        }
        else if (jugador.velocity.x < 0)
        {
            spritee.flipX = true;
            blaster.transform.position = new Vector3(transform.position.x - 1.188f, blaster.transform.position.y, blaster.transform.position.z);
            upblaster.transform.position = new Vector3(transform.position.x - 0.25f, upblaster.transform.position.y, upblaster.transform.position.z);


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
           // shottan = false;
           
        }
        else up = false;

        if (jugador.velocity.x > 0.005f || jugador.velocity.x < -0.005f)
        {
            runnin = true;
        }
        else if (jugador.velocity.x < 0.005f && jugador.velocity.x > -0.005f)
        {
            runnin = false;
        }

            if (Input.GetKey(KeyCode.Mouse0))
           {
            intervaloBala += Time.deltaTime;

            shottan = true;

            if (TocandoSuelo() == true)
            {
                if (runnin)
                {
                    animator.Play("runNshoot");
                    up = false;
                    if (intervaloBala >= 0.2f)
                    {
                        if (charged == true)
                        {
                        }
                        else
                        Instantiate(bullet, blaster.transform.position, Quaternion.identity);
                        intervaloBala = 0;
                    }

                }
                else if (!runnin)
                {

                    if (up == true)
                    {



                        if (charged == true)
                        {
                            animator.Play("chargeUP");
                            spritee.color = Color.yellow;

                        }
                        else if (intervaloBala >= 0.2f)
                        {
                            Instantiate(upbullet, upblaster.transform.position, Quaternion.identity);
                            intervaloBala = 0;
                        }
                        

                        up = true;
                    }
                    else if (up == false)
                    {

                        if (charged == true)
                        {
                            animator.Play("charge");
                            spritee.color = Color.yellow;

                        }
                        else
                            animator.Play("shoot");

                        up = false;
                    }
                }
            }
            else
            {
                animator.Play("onAir");
                if (up == true)
                {

                    if (intervaloBala >= 0.2f)
                    {
                        Instantiate(upbullet, upblaster.transform.position, Quaternion.identity);
                        intervaloBala = 0;
                    }
                    up = true;
                }
                else if (up == false)
                {
                    if (intervaloBala >= 0.2f)
                    {
                        Instantiate(bullet, blaster.transform.position, Quaternion.identity);
                        intervaloBala = 0;
                    }
                }
            }

            

           }
           else
           {
            intervaloBala = 0;
            shottan = false;

        }
        //Debug.Log(intervaloBala);

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (charged)
            {
                if (!runnin)
                {
                    if (up == false)
                        Instantiate(super, blaster.transform.position, Quaternion.identity);
                    else if (up == true)
                        Instantiate(super, upblaster.transform.position, Quaternion.identity);
                }
                else
                Instantiate(super, blaster.transform.position, Quaternion.identity);
                
                spritee.color = Color.white;
                DecrementarPowerUps();
                print("pingas");
            }


        }

    }

    public void Shoot()
    {
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

    public void IncrementarPowerUps()
    {
        numeroPowerUps++;
        hud.SetPower(numeroPowerUps);
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
           if (!runnin)
        {
           
            
           if (up != true)
           {
                if (shottan != true)

                    animator.Play("Iddle");

           }
                else if(charged != true)
                animator.Play("shootUP");

                animator.SetBool("aire", false);
                //shottan = false;
            
           

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
