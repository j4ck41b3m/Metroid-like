using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemigo : MonoBehaviour
{
    public GameObject Enemigo;
    public float velocidad;
    public Vector3 posicionInicial;
    public Vector3 posicionFinal;
    private float duracionTemblor;
    public bool moviendoAFin, vulnerable;
    public int vidas;
    private SpriteRenderer spritee;
    // Start is called before the first frame update
    void Start()
    {
        vulnerable = true;
        posicionInicial = transform.position;
        posicionFinal = new Vector3(posicionInicial.x, posicionInicial.y + 4, posicionInicial.z);
        moviendoAFin = true;
        velocidad = 5.5f;
        duracionTemblor = 1;
        spritee = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoverEnemigo();
    }

    private void MoverEnemigo()
    {
        Vector3 posicionDestino = (moviendoAFin) ? posicionFinal : posicionInicial;
        transform.position = Vector3.MoveTowards(transform.position, posicionDestino, velocidad * Time.deltaTime);
        if (transform.position == posicionFinal)
        {
            moviendoAFin = false;
        }
        if (transform.position == posicionInicial)
        {
            moviendoAFin = true;
        }
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
                Destroy(gameObject);
            }
            spritee.color = Color.red;
            Invoke("HacerVulnerable", 1f);
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<RPlayer>().QuitarVidas();


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
