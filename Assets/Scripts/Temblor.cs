using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temblor : MonoBehaviour
{
    float duracionTemblor
        ;
    // Start is called before the first frame update
    void Start()
    {
        duracionTemblor = 1f;

    }

    // Update is called once per frame
    private void Update()
   {
       StartCoroutine(TemblorPantalla());
   }

   IEnumerator TemblorPantalla()
   {
       Vector3 PosicionInicial = this.transform.position;
        float tiempoTranscurrido = 0f ;

       while (tiempoTranscurrido < duracionTemblor)
       {
           tiempoTranscurrido += Time.deltaTime;
           transform.position = PosicionInicial + Random.insideUnitSphere;
           yield return null;
       }
        transform.position = PosicionInicial;
   }
}
