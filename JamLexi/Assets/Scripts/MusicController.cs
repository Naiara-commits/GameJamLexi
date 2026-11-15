using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioClip[] tracks = new AudioClip[5];
    [SerializeField, Range(0f, 1f)] private float maxVolume = 1f;
    [SerializeField] private float fadeDuration = 2f;
    [SerializeField] private int firstScreenNumber = 1;

    private AudioSource[] sources;
    private Coroutine[] fades;

    private void Awake()
    {
        sources = new AudioSource[tracks.Length];
        fades = new Coroutine[tracks.Length];

        for (int i = 0; i < tracks.Length; i++)
        {
            sources[i] = gameObject.AddComponent<AudioSource>();
            sources[i].clip = tracks[i];
            sources[i].loop = true;
            sources[i].playOnAwake = false;
            sources[i].volume = 0f;
        }
    }
    private void Start()
    {
        double startTime = AudioSettings.dspTime + 0.2;
        foreach (AudioSource source in sources)
        {
            if (source.clip != null) source.PlayScheduled(startTime);
        }

        PlayTrackForScreen(firstScreenNumber);
    }


    public void PlayTrackForScreen(int screenNumber)
    {
        int layers = screenNumber - firstScreenNumber + 1;
        Debug.Log($"Música: pantalla {screenNumber} -> {layers} capas activas.", this);

        for (int i = 0; i < sources.Length; i++)
        {
            float target = (i < layers) ? maxVolume : 0f;
            FadeTo(i, target);
        }
    }
    private void FadeTo(int index, float target)
    {
        if (fades[index] != null) StopCoroutine(fades[index]);
        fades[index] = StartCoroutine(FadeRoutine(sources[index], target));
    }
    private IEnumerator FadeRoutine(AudioSource source, float target)
    {
        float start = source.volume;
        for (float t = 0f; t < fadeDuration; t += Time.unscaledDeltaTime)
        {
            source.volume = Mathf.Lerp(start, target, t / fadeDuration);
            yield return null;
        }
        source.volume = target;
    }
}
