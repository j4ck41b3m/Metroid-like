using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlDatosJuego : MonoBehaviour
{
    public int puntuacion;
    public bool ganado;


    public int Puntuacion { get => puntuacion; set => puntuacion = value; }

    public bool Ganado { get => ganado; set => ganado = value; }

    private void Awake()
    {
       /* int numInstancias = FindObjectOfType<ControlDatosJuego>()   ;
        if (numInstancias != 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }*/
    }
}
