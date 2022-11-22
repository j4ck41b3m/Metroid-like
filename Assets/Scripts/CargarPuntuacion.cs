using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargarPuntuacion : MonoBehaviour
{
    private ControlDatosJuego datosjuego;
    // Start is called before the first frame update
    void Start()
    {
        datosjuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
