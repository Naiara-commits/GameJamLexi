using UnityEngine;

public class ScreenController : MonoBehaviour
{
    [SerializeField] private Screen[] screens = new Screen[5];
    [SerializeField] private ScreenNumberEvent onScreenEntered = new ScreenNumberEvent();

    public void ReceiveScreen(int screenNumber)
    {
        Screen screen = null;

        foreach (Screen candidate in screens)
        {
            if (candidate != null && candidate.ScreenNumber == screenNumber)
            {
                screen = candidate;
                break;
            }
        }

        if (screen == null)
        {
            Debug.LogError($"No se encontró la pantalla {screenNumber}.", this);
            return;
        }

        SpriteRenderer sprite = screen.GetComponent<SpriteRenderer>();
        if (sprite == null)
        {
            Debug.LogError($"La pantalla {screenNumber} no tiene SpriteRenderer.", screen);
            return;
        }

        sprite.enabled = false;
        onScreenEntered.Invoke(screenNumber);
        Debug.Log($"Se ha entrado en la pantalla {screenNumber}.", screen);
    }
}