using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controlBotones : MonoBehaviour
{
    public void OnJugar()
    {
        SceneManager.LoadScene(1);
    }

    public void OnSalir()
    {
        Application.Quit();
    }

    public void OnMenu()
    {
        SceneManager.LoadScene(0);

    }

    public void OnCredits()
    {
        SceneManager.LoadScene(4);

    }

    public void OnStory()
    {
        SceneManager.LoadScene(5);
    }
}
