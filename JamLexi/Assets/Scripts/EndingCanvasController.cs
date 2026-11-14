using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCanvasController : MonoBehaviour
{
    [SerializeField] public GameObject endingCanvas;

    private bool concluded = false;

    public void Awake()
    {
        endingCanvas.SetActive(false);
     
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Victoria();
        }
    }
    public void Victoria()
    {
        endingCanvas.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void VolverAEmpezar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void VolverMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
}
