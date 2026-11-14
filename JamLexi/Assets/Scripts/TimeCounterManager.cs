using System;
using TMPro;
using UnityEngine;

public class TimeCounterManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private int minutes;
    [SerializeField] private float seconds;
    private bool isRunning = true;
   void Start()
   {
        UploadTimeCounter();
   }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        seconds += Time.deltaTime;
        if (seconds >= 60)
        {
            seconds = 0;
            minutes += 1;
        }
        UploadTimeCounter();
    }

    public void UploadTimeCounter()
    {
        int currentSeconds = Mathf.FloorToInt(seconds);
        counterText.text = string.Format("{0}:{1:00}", minutes, currentSeconds);
    }

    public void EndCounter()
    {
        isRunning = false;
    }
}
