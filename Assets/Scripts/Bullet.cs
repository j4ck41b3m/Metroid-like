using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool derecha;
    private bool up;
    public int speed;
    public float daño;
    //private float velo;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 5f);
        player = GameObject.Find("Player");
        derecha = player.GetComponent<RPlayer>().spritee.flipX;
        up = player.GetComponent<RPlayer>().up;
        speed = 20;

    }

    // Update is called once per frame
    void Update()
    {
        if (up == false)
        {
            if (derecha == false)
            {
                gameObject.transform.Translate(speed * Time.deltaTime, 0, 0);

            }
            else if (derecha == true)
            {
                gameObject.transform.Translate(-1 * speed * Time.deltaTime, 0, 0);

            }
        }
        else
            gameObject.transform.Translate(0, 1 * speed * Time.deltaTime, 0);

       // Debug.Log("Es " + derecha);

    }
    
    

   private void Shoot()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Solid")|| collision.gameObject.CompareTag("Enemy"))
        {
            DestroyBullet();

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<enemigo>().QuitarVidas(daño);


        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
