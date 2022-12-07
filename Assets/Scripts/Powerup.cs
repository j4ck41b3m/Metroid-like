using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    public int cantidad;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<RPlayer>().IncrementarPuntuacion(cantidad);
            collision.gameObject.GetComponent<RPlayer>().IncrementarPowerUps();

            Destroy(gameObject);
        }
    }
}
