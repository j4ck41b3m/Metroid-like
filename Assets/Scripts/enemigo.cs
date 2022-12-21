using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public GameObject punto, mouth, blast, boom;
    public float velocidad;
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    private float duracionTemblor, intervalo;
    public bool moviendoAFin, vulnerable, crab, craw;
    public float vidas;
    private SpriteRenderer spritee;
    private AudioSource Eaudio;
    public AudioClip blas;
    // Start is called before the first frame update
    void Start()
    {

        Eaudio = GetComponent<AudioSource>();
        vulnerable = true;
        posicionInicial = transform.position;
        posicionFinal = punto.transform.position ;
        //posicionFinal = new Vector3(posicionInicial.x + 4, posicionInicial.y, posicionInicial.z);
        moviendoAFin = true;
        //velocidad = 4f;
        duracionTemblor = 1;
        spritee = GetComponent<SpriteRenderer>();
        if (crab == true)
            spritee.flipX = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
        if (craw == true)
        {
            intervalo += Time.deltaTime;
            if (intervalo >= 3)
            {
                Instantiate(blast, mouth.transform.position, Quaternion.identity);
                intervalo = 0;
                Eaudio.PlayOneShot(blas);
            }
        }
    }

    private void MoverEnemigo()
    {
        Vector3 posicionDestino = (moviendoAFin) ? posicionFinal : posicionInicial;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        if (transform.position == posicionFinal)
        {
            moviendoAFin = false;
            if(crab)
            spritee.flipX= false;
        }
        if (transform.position == posicionInicial)
        {
            moviendoAFin = true;
            if(crab)
            spritee.flipX = true;

        }
    }

    private void HacerVulnerable()
    {
        vulnerable = true;
        spritee.color = Color.white;
    }
    public void QuitarVidas(float daño)
    {
        if (vulnerable)
        {
            vulnerable = false;
            vidas -= daño;
            if (vidas <= 0)
            {
                spritee.enabled = false;
                Instantiate(boom, transform.position, Quaternion.identity);

                Invoke("Destroy", 0.01f);
            }
            spritee.color = Color.red;
            Invoke("HacerVulnerable", 0.01f);
        }


    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<RPlayer>().QuitarVidas(1);


        }
        //StartCoroutine(TemblorPantalla());
    }

    /*IEnumerator TemblorPantalla()
    {
        Vector3 PosicionInicial = this.transform.position;
        float tiempoTranscurrido = 0f;

        while (tiempoTranscurrido < duracionTemblor)
        {
            tiempoTranscurrido += Time.deltaTime;
            transform.position = PosicionInicial + Random.insideUnitSphere;
            yield return null;
        }
        transform.position = PosicionInicial;
    }*/
}
