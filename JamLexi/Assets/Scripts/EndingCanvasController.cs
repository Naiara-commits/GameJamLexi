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

//A lo que sea la meta se le pone un collider y se marca is trigger. Este script va en el objeto final y mete el canvas.
//Al jugador se le pone la tag del player y se le mete rigidbody 
//Añadir dos botones al canvas, uno de volver a empezar y otro de volver al menu principal

