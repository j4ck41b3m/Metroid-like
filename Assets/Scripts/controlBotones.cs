using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlBotones : MonoBehaviour
{
    public void OnJugar()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void OnSalir()
    {
        Application.Quit();
    }

    public void OnMenu()
    {
        SceneManager.LoadScene("Menu");

    }

    public void OnCredits()
    {
        SceneManager.LoadScene("Creditos");

    }
}
