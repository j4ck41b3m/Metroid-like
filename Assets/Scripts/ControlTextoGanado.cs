using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ControlTextoGanado : MonoBehaviour
{
    public TextMesh puntuacionTxt;
    private ControlDatosJuego datosjuego;

    private void Start()
    {
        datosjuego = GameObject.Find("DatosJuego").GetComponent<ControlDatosJuego>();

    }
    public void SetPuntuacion(int puntos)
    {
        puntuacionTxt.text = "Puntos" + puntos;
    }
}
