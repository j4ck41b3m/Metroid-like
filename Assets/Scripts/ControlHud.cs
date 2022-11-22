using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlHud : MonoBehaviour
{
    public TextMeshProUGUI vidasTxt;
    public TextMeshProUGUI powerTxt; 
    public TextMeshProUGUI timeTxt;

    public void SetVidas(int vidas)
    {
        vidasTxt.text = "Vidas:" + vidas;
    }

    public void SetTiempo(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60; ;


        timeTxt.text = minutos + ":" + segundos;
    }

    public void SetPower(int power)
    {
        powerTxt.text = "Power Up:" + power;
       
    }
}
