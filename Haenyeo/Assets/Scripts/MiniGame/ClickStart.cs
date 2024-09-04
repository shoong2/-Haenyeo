using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ClickStart : MonoBehaviour
{
    public Button Start_cook;
    public RectTransform MiniGame_ver_bar; // 세로 작은 바
    public RectTransform MiniGame_bar; // 가로 큰 바
    public RectTransform MiniGame_Gauge; // 움직임 영역을 제한할 게이지 오브젝트

    public GameObject greatResultImage; // Canvas3/Result_image/great 오브젝트
    public GameObject badResultImage;   // Canvas3/Result_image/bad 오브젝트
    public GameObject timeoverResultImage;

    private AddItem addItemScript; // AddItem 스크립트 참조
    private ClickRecipeItem clickRecipeItemScript; //ClickRecipeItem 스크립트 참조
    private ClickRecipe clickRecipeScript;
    private ClickFish clickFishScript;
    public TimeManager timerManager;

    public float speedMultiplier = 5f; // 속도 배율
    
    private bool isMoving = false;
    private float minX; // 움직임 영역의 최소 X 좌표
    private float maxX; // 움직임 영역의 최대 X 좌표
    private float moveRange; // 움직임 범위
    private float centerX; // MiniGame_Gauge의 중앙 X 좌표


    void Start()
    {
        Start_cook.onClick.AddListener(OnClickButton);

        if (timerManager == null)
        {
            timerManager = FindObjectOfType<TimeManager>();
            if (timerManager == null)
            {
                Debug.LogError("TimerManager not found in the scene!");
            }
        }

        // MiniGame_Gauge의 좌우 경계와 중앙 계산
        float gaugeHalfWidth = MiniGame_Gauge.rect.width / 2;
        centerX = MiniGame_Gauge.anchoredPosition.x;
        minX = centerX - gaugeHalfWidth;
        maxX = centerX + gaugeHalfWidth;
        moveRange = maxX - minX - 250;
        
        // 가로바와 세로바를 중앙에 위치시킴
        SetBarsToCenter();

        // AddItem 스크립트 찾기
        addItemScript = FindObjectOfType<AddItem>();
        clickRecipeItemScript = FindObjectOfType<ClickRecipeItem>();
        clickRecipeScript = FindObjectOfType<ClickRecipe>();
        clickFishScript = FindObjectOfType<ClickFish>();

        if (addItemScript == null || clickRecipeItemScript == null)
        {
            Debug.LogError("Required scripts not found in the scene!");
        }

        // 결과 이미지 초기 비활성화
        greatResultImage.SetActive(false);
        badResultImage.SetActive(false);
        timeoverResultImage.SetActive(false);
        
        Debug.Log($"Movement range: {minX} to {maxX}, Center: {centerX}");
        Debug.Log("ClickStart initialized. Button listener added.");
    }

    void SetBarsToCenter()
    {
        MiniGame_ver_bar.anchoredPosition = new Vector2(centerX, MiniGame_ver_bar.anchoredPosition.y);
        MiniGame_bar.anchoredPosition = new Vector2(centerX, MiniGame_bar.anchoredPosition.y);
    }

    void OnClickButton()
    {
        isMoving = !isMoving;
        Debug.Log("Button clicked. isMoving: " + isMoving);
        if (isMoving)
        {
            // 움직임 시작 시 바를 중앙으로 리셋
            SetBarsToCenter();
        }
        else
        {
            StartCoroutine(DelayedCheckResult());
        }
    }

    IEnumerator DelayedCheckResult()
    {
        yield return new WaitForSeconds(0.1f);
        CheckResult();
        yield return new WaitForSeconds(0.5f); // 결과를 1초간 표시
        ResetGame();
    }
    public void ResetGame()
    {
        isMoving = false;
        SetBarsToCenter();
        greatResultImage.SetActive(false);
        badResultImage.SetActive(false);

        // 조리 버튼 비활성화
        Start_cook.interactable = false;

        // RecipeInput과 Recipe_NeedNumber 숨기기
        if (clickRecipeItemScript != null)
        {
            clickRecipeItemScript.RecipeInput.SetActive(false);
            clickRecipeItemScript.Recipe_NeedNumber.SetActive(false);

            // 레시피 아이템 버튼 활성화 (선택적)
            clickRecipeItemScript.RecipeItem_Button.interactable = true;
        }

        // AddItem 초기화
        if (addItemScript != null)
        {
            addItemScript.ResetItems();
        }

        Debug.Log("Game Reset");
        // Recipe_Window 활성화 및 Fish_Window 비활성화
        if (clickRecipeScript != null && clickFishScript != null)
        {
            clickRecipeScript.Recipe_Window.SetActive(true);
            clickFishScript.Fish_Window.SetActive(false);
        }

        Debug.Log("Game Reset and Recipe Window Activated");
    }

    void Update()
    {
        if(isMoving)
        {
            float time = Time.time * speedMultiplier;

            // 세로 바 움직이기 (좌우로 움직임)
            float verBarNewX = centerX + Mathf.Sin(time * 2f) * (moveRange / 2);
            MiniGame_ver_bar.anchoredPosition = new Vector2(verBarNewX, MiniGame_ver_bar.anchoredPosition.y);

            // 가로 바 움직이기 (좌우로 움직임, 위상차를 두어 다르게 움직이게 함)
            float horBarNewX = centerX + Mathf.Sin(time * 1.5f + 1000) * (moveRange / 2);
            MiniGame_bar.anchoredPosition = new Vector2(horBarNewX, MiniGame_bar.anchoredPosition.y);
        }
    }

    void CheckResult()
    {
        float verBarCenter = MiniGame_ver_bar.anchoredPosition.x;
        float horBarLeft = MiniGame_bar.anchoredPosition.x - MiniGame_bar.rect.width / 2;
        float horBarRight = MiniGame_bar.anchoredPosition.x + MiniGame_bar.rect.width / 2;

        bool isOverlapping = verBarCenter >= horBarLeft && verBarCenter <= horBarRight;
        bool isIngredientsCorrect = CheckIngredients();

        if (isOverlapping && isIngredientsCorrect)
        {
            Debug.Log("Great!");
            ShowResult(true);
        }
        else
        {
            Debug.Log("Bad!");
            ShowResult(false);
        }

        Debug.Log($"Vertical Bar Center: {verBarCenter}");
        Debug.Log($"Horizontal Bar: Left={horBarLeft}, Right={horBarRight}");
        Debug.Log($"Judgment: {(isOverlapping ? "Great" : "Bad")}");
    }
    bool CheckIngredients()
    {
        if (addItemScript == null)
        {
            Debug.LogError("AddItem script is not set!");
            return false;
        }

        return addItemScript.a == addItemScript.InputA &&
               addItemScript.b == addItemScript.InputB &&
               addItemScript.c == addItemScript.InputC;
    }

    void ShowResult(bool isGreate)
    {
        greatResultImage.SetActive(isGreate);
        badResultImage.SetActive(!isGreate);
    }
}