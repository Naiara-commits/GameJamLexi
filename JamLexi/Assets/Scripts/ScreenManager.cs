using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public void Jugar()
    {
        SceneManager.LoadScene("PrueNiv");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Credits()
    {
        SceneManager.LoadScene("CreditsScene");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
    public void Salir()
    {
        Application.Quit();
    }
}
