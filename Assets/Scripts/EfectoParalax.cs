using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoParalax : MonoBehaviour
{
    public float efectoParalax;
    private Transform camara;
    public GameObject player;
    private Vector3 camaraUltimaPos;
    // Start is called before the first frame update
    void Start()
    {
        //camara = Camera.main.transform;
        camara = player.transform;
        camaraUltimaPos = camara.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 movimientoFondo = camara.position - camaraUltimaPos;
        transform.position += new Vector3(movimientoFondo.x * efectoParalax, movimientoFondo.y * 0.1f, 0);
        camaraUltimaPos = camara.position;
    }
}
