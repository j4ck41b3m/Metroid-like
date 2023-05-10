using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RPlayer : MonoBehaviour
{
    public float velocidad;
    public Rigidbody2D jugador;
    public SpriteRenderer spritee;
    private SpriteRenderer blast, upblast, runsu;
    private float fuerzaSalto;
    private Animator animator;
    public int puntuacion, vector;
    public int vidas;
    public bool vulnerable, shottan, up, charged, runnin, cargao;
    public int numeroPowerUps;

    public int tiempoNivel;
    private float tiempoInicio, intervaloBala, tiempoCarga, interSalto;
    private int tiempoEmpleado;
    
    public Canvas canvas;
    private ControlHud hud;

    public AudioClip sonidoPower, sonidoCarga, sonidoDisparo, sonidoSuper, sonidoSalto, sonidoDaño;


    private AudioSource audioP;
    public ControlDatosJuego datosJuego;

    public LayerMask Ground;

    public GameObject blaster, upblaster, bullet, upbullet, super, runsuper, Apanel, Bpanel;
    public Button B_shoot, B_jump, B_up, B_left, B_right;
    // Start is called before the first frame update
    void Start()
    {
        blast = blaster.GetComponent<SpriteRenderer>();
        upblast = upblaster.GetComponent<SpriteRenderer>();
        runsu = runsuper.GetComponent<SpriteRenderer>();

        jugador = GetComponent<Rigidbody2D>();
        spritee = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        velocidad = 7.5f;
        fuerzaSalto = 11f;

        vulnerable = true;

        tiempoInicio = Time.time;

        hud = canvas.GetComponent<ControlHud>();
        hud.SetPower(numeroPowerUps);
        hud.SetVidas(vidas);

        datosJuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();

        vector = 0;
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
        if (jugador.velocity.x > 0.005f || jugador.velocity.x < -0.005f)
        {
            runnin = true;
        }
        else if (jugador.velocity.x < 0.005f && jugador.velocity.x > -0.005f)
        {
            runnin = false;
        }
#if UNITY_STANDALONE
        Apanel.SetActive(false);
        Bpanel.SetActive(false);
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo() == true)
        {
            jugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
        //////////////// D I S P A R O S////////////////

        if (Input.GetKey(KeyCode.W))
        {
            up = true;
           // shottan = false;
           
        }
        else up = false;

       
        

            if (Input.GetKey(KeyCode.Mouse0))
           {
            intervaloBala += Time.deltaTime;

            shottan = true;

            if (charged)
            {
                audioP.PlayOneShot(sonidoCarga, 0.05f);

                tiempoCarga += Time.deltaTime;
                if (tiempoCarga > 2)
                {
                    runsu.color = Color.red;
                    blast.color = Color.red;
                    upblast.color = Color.red;

                    cargao = true;
                }
            }

            if (TocandoSuelo() == true)
            {
                if (runnin)
                {
                    up = false;


                    if (charged == true)
                    {
                        animator.Play("runNshoot");
                        spritee.color = Color.yellow;

                    }
                    else if (intervaloBala >= 0.2f)
                    {
                        audioP.PlayOneShot(sonidoDisparo);

                        Instantiate(bullet, blaster.transform.position, Quaternion.identity);
                        animator.Play("runNcharge");
                        intervaloBala = 0;


                    }

                }
                else if (!runnin)
                {

                    if (up == true)
                    {



                        if (charged == true)
                        {
                           // animator.Play("chargeUP");
                            spritee.color = Color.yellow;

                        }
                        else if (intervaloBala >= 0.2f)
                        {
                            audioP.PlayOneShot(sonidoDisparo);

                            Instantiate(upbullet, upblaster.transform.position, Quaternion.identity);
                            intervaloBala = 0;
                        }

                        //animator.Play("shootUP");


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
                    if (charged)
                    {
                        spritee.color = Color.yellow;

                    }
                    else if (intervaloBala >= 0.2f)
                    {
                        audioP.PlayOneShot(sonidoDisparo);

                        Instantiate(upbullet, upblaster.transform.position, Quaternion.identity);
                        intervaloBala = 0;
                    }
                    up = true;
                }
                else if (up == false)
                {
                    if (charged)
                    {
                        spritee.color = Color.yellow;

                    }
                    else if (intervaloBala >= 0.2f)
                    {
                        audioP.PlayOneShot(sonidoDisparo);

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

                if (cargao)
                {
                    audioP.PlayOneShot(sonidoSuper, 2f);

                    if (TocandoSuelo() == true)
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
                    }
                    else
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
                    }

                        

                    spritee.color = Color.white;
                    runsu.color = Color.white;
                    blast.color = Color.white;
                    upblast.color = Color.white;
                    DecrementarPowerUps();
                    print("pingas");
                    cargao = false;
                }
                


            }
            spritee.color = Color.white;
            tiempoCarga = 0;

        }
#endif
#if UNITY_ANDROID

     
        Apanel.SetActive(true);
        Bpanel.SetActive(true);
        


#endif

    }

    public void Jump()
    {
        if (TocandoSuelo() == true)
        {
            jugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);

        }

    }
    public void Shoot()
    {
        Instantiate(bullet, blaster.transform.position, Quaternion.identity);
        audioP.PlayOneShot(sonidoDisparo);

    }

    public void Left()
    {
        vector = -1;
    }

    public void Right()
    {
        vector = 1;
    }
     
    public void None()
    {
        vector = 0;
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
        audioP.PlayOneShot(sonidoPower);

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
            interSalto += Time.deltaTime;
            if (interSalto >= 0.25f)
            {
                audioP.PlayOneShot(sonidoSalto);
                interSalto = 0;
            }


        }
        else
           if (!runnin)
        {
           
            
           if (up != true)
           {
                if (shottan != true)

                    animator.Play("Iddle");

           }

            if (up)
            {
                if (shottan && charged)
                {
                    animator.Play("chargeUP");

                }
                else animator.Play("shootUP");

            }


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
        audioP = GetComponent<AudioSource>();
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

    public void QuitarVidas(int daño)
    {
        if (vulnerable)
        {
            vulnerable = false;
            vidas -= daño;

            if (vidas <= 0)
            {
                FinJuego();
            }
            hud.SetVidas(vidas);
            audioP.PlayOneShot(sonidoDaño);
            spritee.color = Color.red;
            Invoke("HacerVulnerable", 1f);
        }
        
        
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bound"))
            FinJuego();
    }


    public void FinJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
