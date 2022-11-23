using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private bool derecha;
    //private float velo;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 5f);
        GetComponent<RPlayer>();

    }

    // Update is called once per frame
    void Update()
    {
        derecha = GetComponent<RPlayer>().spritee.flipX;

        if (derecha == true)
        {
            gameObject.transform.Translate(10 * Time.deltaTime, 0, 0);

        }
        else if (derecha == false)
        {
            gameObject.transform.Translate(-10 * Time.deltaTime, 0, 0);

        }
        Debug.Log(derecha);

    }
    
    

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Solid")|| collision.gameObject.CompareTag("Enemy"))
        {
            DestroyBullet();

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<enemigo>().QuitarVidas();


        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
