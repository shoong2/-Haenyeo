using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Start_cook : MonoBehaviour
{
    public GameObject MiniGame_bar; // 바 오브젝트
    public GameObject MiniGame_ver_bar; // 영역 오브젝트
    public TMP_Text TimeCount; // 시간 카운트 텍스트
    public Button cookButton; // 요리하기 버튼
    public GameObject timeImage; // 타임오버 이미지
    public GameObject greatImage; // Great 이미지
    public GameObject goodImage; // Good 이미지
    public GameObject badImage; // Bad 이미지

    private const float MoveRange = 3.5f; // 이동 범위
    public float barMoveSpeed = 0.05f; // 바 이동 속도
    public float verBarMoveSpeed = 0.01f; // 영역 이동 속도

    private bool isMoving = false; // 이동 여부
    private bool isCounting = false; // 카운트다운 여부

    void Start()
    {
        // 오브젝트 초기화
        TimeCount = GameObject.Find("TimeCount").GetComponent<TMP_Text>();
        cookButton = GameObject.Find("MiniGame_CookButton").GetComponent<Button>();
        timeImage = GameObject.Find("Canvas3/Result_image/timeover");
        badImage = GameObject.Find("Canvas3/Result_image/bad");
        goodImage = GameObject.Find("Canvas3/Result_image/good");
        greatImage = GameObject.Find("Canvas3/Result_image/great");

        // 요리하기 버튼에 클릭 이벤트 추가
        cookButton.onClick.AddListener(StartCooking);

        // 이미지들을 비활성화
        timeImage.SetActive(false);
        greatImage.SetActive(false);
        goodImage.SetActive(false);
        badImage.SetActive(false);

        // 씬이 시작될 때 카운트 다운 시작
        StartCoroutine(StartCountdown());
    }

    void StartCooking()
    {
        
        Debug.Log(SeafoodManagerNew.Instance.seafoodCountDict["recipe_numA"]);
        if (isCounting) // 카운트가 진행 중일 때만 실행
        {
            isMoving = !isMoving; // 이동 상태 토글
            Debug.Log("StartCooking: isMoving set to " + isMoving);

            if (!isMoving) // 멈추었을 때 판정 수행
            {
                CheckCookingResult();
            }
        }
    }

    IEnumerator StartCountdown()
    {
        isCounting = true; // 카운트가 시작됨을 표시
        int timeLeft = 60;
        while (timeLeft >= 0)
        {
            TimeCount.text = timeLeft.ToString(); // 시간을 텍스트에 표시
            Debug.Log("Countdown: " + timeLeft); // 디버그 로그 추가
            yield return new WaitForSeconds(1); // 1초 대기
            timeLeft--;
        }
        isCounting = false; // 카운트가 종료됨을 표시
        cookButton.interactable = false; // CookButton 비활성화

        // "timeover" 이미지 활성화
        if (timeImage != null)
        {
            timeImage.SetActive(true);
            Debug.Log("Found timeover image: " + (timeImage != null));
        }
        else
        {
            Debug.LogError("Failed to find 'timeover' image GameObject.");
        }

        // 카운트가 종료되면 MiniGame_bar와 MiniGame_ver_bar를 멈추도록 설정
        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            // 바 이동
            float newBarX = MiniGame_bar.transform.position.x + barMoveSpeed;
            if (newBarX < -MoveRange - 2f || newBarX > MoveRange - 2f)
            {
                barMoveSpeed *= -1;
            }
            MiniGame_bar.transform.Translate(Vector3.right * barMoveSpeed);

            // 영역 이동
            float newVerBarX = MiniGame_ver_bar.transform.position.x + verBarMoveSpeed;
            if (newVerBarX < -MoveRange - 2f || newVerBarX > MoveRange - 2f)
            {
                verBarMoveSpeed *= -1;
            }
            MiniGame_ver_bar.transform.Translate(Vector3.right * verBarMoveSpeed);
        }
    }

    void CheckCookingResult()
    {
        // 필요한 재료와 넣은 재료의 일치 여부 확인
        Dictionary<string, int> seafoodCountDict = SeafoodManagerNew.Instance.seafoodCountDict;
        int seafood1Count = seafoodCountDict["seafood1"];
        int seafood2Count = seafoodCountDict["seafood2"];
        int seafood3Count = seafoodCountDict["seafood3"];
        int recipe_numACount = SeafoodManagerNew.Instance.seafoodCountDict["recipe_numA"];//SeafoodManagerNew.Instance.GetSeafoodCount("recipe_numA");
        int recipe_numBCount = SeafoodManagerNew.Instance.seafoodCountDict["recipe_numB"];//SeafoodManagerNew.Instance.GetSeafoodCount("recipe_numB");
        int recipe_numCCount = SeafoodManagerNew.Instance.seafoodCountDict["recipe_numC"];//SeafoodManagerNew.Instance.GetSeafoodCount("recipe_numC");
        
        Debug.Log(seafood1Count == recipe_numACount);
        Debug.Log(seafood1Count);
        Debug.Log(recipe_numACount);
        Debug.Log(seafood2Count == recipe_numBCount);
        Debug.Log(seafood3Count == recipe_numCCount);
        bool isRecipeCorrect = seafood1Count == recipe_numACount &&
                               seafood2Count == recipe_numBCount &&
                               seafood3Count == recipe_numCCount;

        bool isInVerBarRange = MiniGame_bar.transform.localPosition.x + MiniGame_bar.GetComponent<RectTransform>().rect.width / 2 > MiniGame_ver_bar.transform.localPosition.x &&
                               MiniGame_bar.transform.localPosition.x - MiniGame_bar.GetComponent<RectTransform>().rect.width / 2 <= MiniGame_ver_bar.transform.localPosition.x;

        if (isRecipeCorrect)
        {
            if (isInVerBarRange)
            {
                // Great 이미지 활성화
                StartCoroutine(ShowResultImage(greatImage));
                Debug.Log("Great");
            }
            else
            {
                // Good 이미지 활성화
                StartCoroutine(ShowResultImage(goodImage));
                Debug.Log("Good");
            }
        }
        else
        {
            // Bad 이미지 활성화
            StartCoroutine(ShowResultImage(badImage));
            Debug.Log("Bad");
        }
    }

    IEnumerator ShowResultImage(GameObject image)
    {
        image.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        image.SetActive(false);
    }
}
