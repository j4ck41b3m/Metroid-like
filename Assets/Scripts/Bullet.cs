using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet", 5f);
        GetComponent<RPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
      
        gameObject.transform.Translate(5 * Time.deltaTime, 0,0);
        
    }
    
    

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Solid")
        {
            DestroyBullet();

        }
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
