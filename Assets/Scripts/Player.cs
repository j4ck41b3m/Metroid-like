using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public int velocidad;
    private Rigidbody2D jugador;
    private SpriteRenderer spritee;
    private int fuerzaSalto;
    // Start is called before the first frame update
    void Start()
    {
        jugador = GetComponent<Rigidbody2D>();
        spritee = GetComponent<SpriteRenderer>();
        velocidad = 10;
        fuerzaSalto = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (jugador.velocity.x > 0)
        {
            spritee.flipX = false;
        }
        else if (jugador.velocity.x < 0)
        {
            spritee.flipX=true;
        }
        Debug.Log(jugador.velocity.x);

    }
    private void FixedUpdate()
    {
        float entradaX = Input.GetAxis("Horizontal");
        
        jugador.velocity = new Vector2(entradaX * velocidad, jugador.velocity.y);
        
        if (Input.GetKeyDown(KeyCode.Space) && TocandoSuelo())
        {
            jugador.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        }
        
    }
    private bool TocandoSuelo()
    {

        RaycastHit2D toca = Physics2D.Raycast(transform.position + new Vector3(0, -2f, 0)
            , Vector2.down, 0.2f);
        return toca.collider != null;

    }
    public void FinJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
                 