using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate : MonoBehaviour
{
    private Animator anima;
    public GameObject[] enemies;
    public int numEn;
    public GameObject canvas, toNext;
    private ControlHud hud;
    private AudioSource audioP;
    public AudioClip sonidoClear;



    // Start is called before the first frame update
    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        audioP = GetComponent<AudioSource>();
        anima = GetComponent<Animator>();
        canvas = GameObject.Find("Canvas");
        hud = canvas.GetComponent<ControlHud>();
        hud.SetThenMonster(enemies.Length);
      
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        numEn = enemies.Length;

        hud.SetNowMonster(numEn);
        if(numEn == 0)
        {
            audioP.PlayOneShot(sonidoClear, 0.05f);

        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (numEn == 0)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                anima.Play("gate");
            }
            else
                anima.Play("closed");
            toNext.SetActive(true);
        }
        else if(numEn != 0)
        {
            anima.Play("closed");
            toNext.SetActive(false);
        }
            

        

    }

    
}
