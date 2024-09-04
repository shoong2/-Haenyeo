using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public float gameDuration = 20f; // 게임 시간 (초)
    private float timeRemaining;
    public TextMeshProUGUI timerText; // TextMeshProUGUI 컴포넌트 사용
    public GameObject timeoverResultImage;
    public ClickStart clickStartScript;

    private void Start()
    {
        timeRemaining = gameDuration;
        if (timerText == null)
        {
            timerText = GameObject.Find("Canvas/MiniGame_Time/TimeCount").GetComponent<TextMeshProUGUI>();
        }
        clickStartScript = FindObjectOfType<ClickStart>();
        if (clickStartScript == null)
        {
            Debug.LogError("ClickStart script not found in the scene!");
        }
        UpdateTimerDisplay();
        timeoverResultImage.SetActive(false);
    }

    private void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            UpdateTimerDisplay();
            GameOver();
        }
    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null)
        {
            int minutes = Mathf.FloorToInt(timeRemaining / 60);
            int seconds = Mathf.FloorToInt(timeRemaining % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogError("Timer Text is not assigned!");
        }
    }

    private void GameOver()
    {
        timeoverResultImage.SetActive(true);
        if (clickStartScript != null)
        {
            clickStartScript.ResetGame();
        }
        else
        {
            Debug.LogError("ClickStart script is null. Cannot reset game!");
        }
        Invoke("LoadKitchenScene", 2f);
    }

    private void LoadKitchenScene()
    {
        SceneManager.LoadScene("Kitchen");
    }
}