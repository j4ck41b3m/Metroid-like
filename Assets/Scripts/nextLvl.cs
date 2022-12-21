using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class nextLvl : MonoBehaviour
{
    public int Nivel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Invoke("LoadLvl", 0.25f);
        }
    }

    public void LoadLvl()
    {
        SceneManager.LoadScene(Nivel);

    }
}
