using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{
    [SerializeField] public GameObject pauseCanvas;

    private bool isPaused = false;

    public void Awake()
    {
        pauseCanvas.SetActive(false);
    }

    void Start()
    {
        Seguir(); 
    }
    
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                Seguir();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Pause()
    {
        isPaused = true;
        Time.timeScale = 0f;           // Para el juego
        pauseCanvas.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Seguir()
    {
        isPaused = false;
        Time.timeScale = 1f;
        pauseCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void EmpezarNuevo()
    {
        Time.timeScale = 1f;           //Resetea el juego desde 0
        SceneManager.LoadScene("GameScene");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void VolverMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
