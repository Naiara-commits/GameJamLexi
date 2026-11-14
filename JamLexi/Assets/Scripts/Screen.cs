using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ScreenNumberEvent : UnityEvent<int> { }

[RequireComponent(typeof(BoxCollider2D))]

public class Screen : MonoBehaviour
{
    [SerializeField] private int screenNumber;
    [SerializeField] private ScreenNumberEvent onPlayerEntered = new ScreenNumberEvent();
    
    public int ScreenNumber => screenNumber;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))return;

        onPlayerEntered.Invoke(screenNumber);
    }
}