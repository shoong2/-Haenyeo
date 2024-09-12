using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; 

public class TimeManager : MonoBehaviour
{
    public float gameDuration = 20f; // 게임 시간 (초)
    private float timeRemaining;
    public TextMeshProUGUI timerText; // TextMeshProUGUI 컴포넌트 사용
    public GameObject timeoverResultImage;
    public ClickStart clickStartScript;

    public GameObject startImage;  // 추가: 시작 이미지 오브젝트
    public float startImageDuration = 2f;  // 추가: 시작 이미지 표시 시간
    private bool isGameStarted = false;  // 추가: 게임 시작 상태

    // 애니메이션을 위한 새로운 변수들
    public float animationDuration = 1f;
    public float centerDuration = 1f;
    private RectTransform startImageRect;
    private Canvas canvas3;


    private void Start()
    {
        // 타이머 텍스트를 초기에 비활성화
        if (timerText != null)
        {
            timerText.gameObject.SetActive(false);
        }

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

        // 추가: 시작 이미지 표시 및 게임 시작
        startImageRect = startImage.GetComponent<RectTransform>();
        canvas3 = GameObject.Find("Canvas3").GetComponent<Canvas>();
        if (canvas3 == null)
        {
            Debug.LogError("Canvas3 not found!");
            return;
        }
        StartCoroutine(StartGameWithAnimatedImage());
    }

        private IEnumerator StartGameWithAnimatedImage()
    {
        RectTransform canvasRect = canvas3.GetComponent<RectTransform>();
        Vector2 canvasSize = canvasRect.rect.size;
        
        Vector2 startPos = new Vector2(canvasSize.x, 0);
        Vector2 centerPos = Vector2.zero;
        Vector2 endPos = new Vector2(-canvasSize.x, 0);

        startImageRect.anchorMin = new Vector2(0.5f, 0.5f);
        startImageRect.anchorMax = new Vector2(0.5f, 0.5f);
        startImageRect.anchoredPosition = startPos;
        startImage.SetActive(true);

        yield return StartCoroutine(MoveImage(startPos, centerPos, animationDuration));
        yield return new WaitForSeconds(centerDuration);
        yield return StartCoroutine(MoveImage(centerPos, endPos, animationDuration));

        startImage.SetActive(false);

        // 타이머 텍스트 활성화 및 게임 시작
        if (timerText != null)
        {
            timerText.gameObject.SetActive(true);
        }
        isGameStarted = true;
    }

    private IEnumerator MoveImage(Vector2 start, Vector2 end, float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            startImageRect.anchoredPosition = Vector2.Lerp(start, end, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        startImageRect.anchoredPosition = end;
    }

    // 추가: 시작 이미지 표시 및 게임 시작 코루틴
    private IEnumerator StartGameWithImage()
    {
        startImage.SetActive(true);
        yield return new WaitForSeconds(startImageDuration);
        startImage.SetActive(false);
        isGameStarted = true;
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