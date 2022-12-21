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
    public TextMeshProUGUI StartMonsterTxt;
    public TextMeshProUGUI CurrentMonsterTxt;
    public Image ball;



    public void SetVidas(int vidas)
    {
        vidasTxt.text = ":" + vidas;
    }

    public void SetTiempo(int tiempo)
    {
        int segundos = tiempo % 60;
        int minutos = tiempo / 60; ;


        timeTxt.text = minutos + ":" + segundos;
    }

    public void SetPower(int power)
    {
        powerTxt.text = ":" + power;
       
    }

    public void SetThenMonster(int monster)
    {
        StartMonsterTxt.text = "/ " + monster;
    }

    public void SetNowMonster(int left)
    {
        CurrentMonsterTxt.text = ":" + left;
        if (left == 0)
        {
            SetBall();
        }
    }

    public void SetBall()
    {
        
        
            ball.color = Color.blue;
        
    }
}
