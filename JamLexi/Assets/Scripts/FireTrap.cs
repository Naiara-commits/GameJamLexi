using UnityEngine;
using System.Collections;

public class FireTrap : MonoBehaviour
{
    [SerializeField] public GameObject fire;
    [SerializeField] public float ActiveTime = 3f;
    [SerializeField] public float RestTime = 1.5f;
    [SerializeField] public float SyncTime = 0f;    //Se pueden desincronizar los fuegos para que no todos vayan a la vez

    private WaitForSeconds activeWait;
    private WaitForSeconds restWait;
    private void Awake()
    {
        activeWait = new WaitForSeconds(ActiveTime);
        restWait = new WaitForSeconds(RestTime);

    }

    private void OnEnable()
    {
        if (fire == null) return;
        StartCoroutine(Fueguitos());
    }
 
    private IEnumerator Fueguitos()
    {
        fire.SetActive(false);  //Se empieza con el fuego apagado 

        if(SyncTime > 0f)       //Esperar para desincronizar 
        yield return new WaitForSeconds(SyncTime);
    
        while (true)        //Bucle para apagar y encender
        {
            fire.SetActive(true);
            yield return activeWait;

            fire.SetActive(false);
            yield return restWait;

        }
    }
}
//Este script va en todos los fuegos y hay que meterles el collider 
