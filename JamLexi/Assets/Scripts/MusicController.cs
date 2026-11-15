using UnityEngine;

public class MusicController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] tracks = new AudioClip[5];

    public void PlayTrackForScreen(int screenNumber)
    {
        int index = screenNumber - 1;

        if (index < 0 || index >= tracks.Length || tracks[index] == null)
        {
            Debug.LogWarning($"No hay pista para la pantalla {screenNumber}.", this);
            return;
        }
        if (audioSource.clip == tracks[index] && audioSource.isPlaying) return;

        audioSource.clip = tracks[index];
        audioSource.loop = true;
        audioSource.Play();
    }
}
