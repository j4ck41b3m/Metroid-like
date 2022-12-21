using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kaboom : MonoBehaviour
{
    private AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        audio.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void todest()
    {
        Invoke("Destroy", 0.5f);
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}
