using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoParalax : MonoBehaviour
{
    public float efectoParalax;
    private Transform camara;
    private Vector3 camaraUltimaPos;
    // Start is called before the first frame update
    void Start()
    {
        camara = Camera.main.transform;
        camaraUltimaPos = camara.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 movimientoFondo = camara.position - camaraUltimaPos;
        transform.position += new Vector3(movimientoFondo.x * efectoParalax, movimientoFondo.y, 0);
        camaraUltimaPos = camara.position;
    }
}
