using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LeaderBoardManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] namesText = new TextMeshProUGUI[10];
    [SerializeField] private TextMeshProUGUI[] timesText = new TextMeshProUGUI[10];
    [SerializeField] private string[] names = new string[10] { " ", " ", " ", " ", " ", " ", " ", " ", " ", " "};
    [SerializeField] private int[] times = new int[10] { 90, 90, 90, 90, 90, 90, 90, 90, 90, 90};

    [SerializeField] TMP_InputField inputField;
    private int pendingTime;
    void Start()
    {
        UploadLeaderBoard();
        OrderLeaderBoard();
        UploadUI();
    }

    public void UploadUI()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i < namesText.Length) namesText[i].text = names[i];

            if (i < timesText.Length)
            {
                if (times[i] >= 999)
                {
                    timesText[i].text = "--:--";
                }
                else
                {
                    int min = times[i] / 60;
                    int sec = times[i] % 60;
                    timesText[i].text = string.Format("{0}:{1:00}", min, sec);
                }
            }
        }
    }
    public void OrderLeaderBoard()
    {
        for (int i = 0; i < times.Length - 1; i++)
        {
            for (int j = i+1 ; j < times.Length; j++)
            {
                if (times[i] > times[j])
                {
                    int temporalTime = times[i];
                    times[i] = times[j];
                    times[j] = temporalTime;

                    string temporalName = names[i];
                    names[i] = names[j];
                    names[j] = temporalName;
                }
            }
        }
    }
    public void AddTime(string newName, int newTime)
    {
        int insertIndex = -1;
        for (int i = 0; i < times.Length; i++)
        {
            if (newTime < times[i])
            {
                insertIndex = i;
                break;
            }
        }
        if (insertIndex != -1)
        {
            for (int i = times.Length - 1; i > insertIndex; i--)
            {
                times[i] = times[i - 1];
                names[i] = names[i - 1];
            }
            times[insertIndex] = newTime;
            names[insertIndex] = newName;

            OrderLeaderBoard();
            UploadUI();
            SaveLeaderBoard();
        }
    }
    public void PrepareTime(int gameTime)
    {
        pendingTime = gameTime;
    }
    public void SaveScoreFromInput()
    {
        AddTime(inputField.text, pendingTime);
        inputField.text = "";
    }
    private void SaveLeaderBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            PlayerPrefs.SetString("NamesSave" + i, names[i]);
            PlayerPrefs.SetInt("TimesSave" + i, times[i]);
        }
        PlayerPrefs.Save(); 
    }

    private void UploadLeaderBoard()
    {
        for (int i = 0; i < 10; i++)
        {
            names[i] = PlayerPrefs.GetString("NamesSave" + i, " ");
            times[i] = PlayerPrefs.GetInt("TimesSave" + i, 999);
        }
    }
    public void DeleteData()
    {
        PlayerPrefs.DeleteAll();
    }
}
