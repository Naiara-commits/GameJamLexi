using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks = new AudioClip[5];     //Pistas
    [SerializeField, Range(0f, 1f)] private float maxVolume = 1f;       //Volumen páximo    
    [SerializeField] private float fadeDuration = 2f;                   //lo que tarda en subir y bajar el volumen da las pistas de audio para que no cambie bruscamente y se note mucho
    [SerializeField] private int firstScreenNumber = 1;                 //primera pantalla

    private AudioSource[] sources;
    private Coroutine[] fades;

    private void Awake()
    {
        sources = new AudioSource[tracks.Length];
        fades = new Coroutine[tracks.Length];

        for (int i = 0; i < tracks.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = tracks[i];        //asigna pista
            sources[i].loop = true;             //loop de las pistas
            sources[i].playOnAwake = false;     
            sources[i].volume = 0f;             //empezar en silencio
        }
    }
    private void Start()
    {
        double startTime = AudioSettings.dspTime + 0.2; //marger para que las pistas empiecen a la vez
        foreach (AudioSource source in sources)
        {
            if (source.clip != null) source.PlayScheduled(startTime);
        }

        PlayTrackForScreen(firstScreenNumber);      //se han activado ya las pistas pero suena la que corresponda a la primera pantalla
    }


    public void PlayTrackForScreen(int screenNumber)
    {
        int layers = screenNumber - firstScreenNumber + 1;      //cuantas capas suenan tipo se van sumando por pantalla, pantalla 1 es 1 capa, pantalla dos son dos capas y así
        Debug.Log($"Música: pantalla {screenNumber} -> {layers} capas activas.", this);

        for (int i = 0; i < sources.Length; i++)
        {
            float target = (i < layers) ? maxVolume : 0f;       //las capas con el indice menos a las "pantallas" suben el volumen al máximo y las otras np
            FadeTo(i, target);
        }
    }
    private void FadeTo(int index, float target)        //inicia la "difuminacion" de la musica
    {
        if (fades[index] != null) StopCoroutine(fades[index]);  //si ya ha empezado uno se cancela
        fades[index] = StartCoroutine(FadeRoutine(sources[index], target)); //empiza el nuevo
    }
    private IEnumerator FadeRoutine(AudioSource source, float target)       ///esta corrutina va cambiando el volumen poco a poco para que no haya un cambio brusco
    {
        float start = source.volume;    //volumen inicial
        for (float t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(start, target, t / fadeDuration);        //lerp para interpolar el volumen nicial y objetivo
            yield return null;
        }
        source.volume = target;     //asegurar el objetivo del volumen
    }
}
