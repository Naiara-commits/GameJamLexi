using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenTransiccion : MonoBehaviour
{
    [SerializeField] CanvasGroup fadeGroup;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float fadeDuration = 0.8f;
    void Start() => StartCoroutine(Fade(1f, 0f)); // fundido de entrada

    public void LoadScene(string sceneName) => StartCoroutine(Transition(sceneName));

    IEnumerator Transition(string sceneName)
    {
        if (audioSource != null) audioSource.Play();
        fadeGroup.blocksRaycasts = true;       // evita pulsar botones mientras funde
        yield return Fade(0f, 1f);             // fundido a negro
        Time.timeScale = 1f;                   // importante si vienes desde la pausa
        SceneManager.LoadScene(sceneName);
    }

    IEnumerator Fade(float from, float to)
    {
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.unscaledDeltaTime;       // funciona aunque el juego esté en pausa
            fadeGroup.alpha = Mathf.Lerp(from, to, t / fadeDuration);
            yield return null;
        }
        fadeGroup.alpha = to;
        if (to == 0f) fadeGroup.blocksRaycasts = false;
    }
}
