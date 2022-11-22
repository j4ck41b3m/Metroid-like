using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RPlayer : MonoBehaviour
{
    public int velocidad;
    private Rigidbody2D jugador;
    private SpriteRenderer spritee;
    private int fuerzaSalto;
    private Animator animator;
    public int puntuacion;
    public int vidas;
    private bool vulnerable;
    public int numeroPowerUps;

    public int tiempoNivel;
    private float tiempoInicio;
    private int tiempoEmpleado;
    
    public Canvas canvas;
    private ControlHud hud;

    public AudioClip sonidoPower;
    public AudioClip sonidoVida;

    private AudioSource audio;
    public ControlDatosJuego datosJuego;

    public GameObject blaster, bullet;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GetComponent<Rigidbody2D>();
        spritee = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        velocidad = 10;
        fuerzaSalto = 6;

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
            blaster.transform.position = new Vector3(transform.position.x + 0.941f, blaster.transform.position.y, blaster.transform.position.z);

        }
        else if (jugador.velocity.x < 0)
        {
            spritee.flipX = true;
            blaster.transform.position = new Vector3(transform.position.x - 0.941f, blaster.transform.position.y, blaster.transform.position.z);


        }
        Debug.Log(TocandoSuelo());
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo() == true)
        {
            jugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
        AnimarJugador();
        Debug.Log(jugador.velocity.x);
        //Debug.Log(Input.GetAxis("Horizontal"));

        tiempoEmpleado = (int)Time.time - (int)tiempoInicio;
        if (tiempoNivel-tiempoEmpleado <0)
        {
            FinJuego();
            Ganado();
        }

        hud.SetTiempo(tiempoEmpleado);
        // hud.SetTiempo(Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.B))
        {
            
            Instantiate(bullet, blaster.transform.position, Quaternion.identity);
        }
    }

    private void Ganado()
    {
        datosJuego.Ganado = true;
        SceneManager.LoadScene("Gamando");
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
            animator.Play("jump");
        else
           if (jugador.velocity.x == 0)
        {
            animator.Play("Iddle");

        }
        else animator.Play("run");
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
            , Vector2.down, 1f);
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
