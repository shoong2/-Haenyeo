using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Start_cook : MonoBehaviour
{
    public GameObject MiniGame_bar;
    public GameObject MiniGame_ver_bar;
    public TMP_Text TimeCount; // TimeCount 텍스트 참조
    public Button cookButton; // CookButton 참조
    public GameObject resultImage; // Result_image GameObject 참조
    public GameObject timeImage; // "bad" 이미지 참조

    private const float MoveRange = 3.5f; // 이동 범위

    public float barMoveSpeed = 0.05f;   // MiniGame_bar의 이동 속도
    public float verBarMoveSpeed = 0.01f; // MiniGame_ver_bar의 이동 속도

    private bool isMoving = false; // 이동 중 여부
    private bool isCounting = false; // 카운트가 진행 중 여부

    private Vector3 barStartPosition;
    private Vector3 verBarStartPosition;

    void Start()
    {
        // TimeCount GameObject 찾기
        TimeCount = GameObject.Find("TimeCount").GetComponent<TMP_Text>();

        // CookButton GameObject 찾기
        cookButton = GameObject.Find("MiniGame_CookButton").GetComponent<Button>();
        
        // CookButton에 클릭 이벤트 핸들러 추가
        cookButton.onClick.AddListener(CookButton_Click);

        // 시작 위치 저장
        barStartPosition = MiniGame_bar.transform.position;
        verBarStartPosition = MiniGame_ver_bar.transform.position;

        // 비활성화된 GameObject 찾기
        /*GameObject[] results = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (GameObject go in results) {
            if (go.name == "Result_image") {
                // 찾았습니다! 이제 여기서 필요한 작업을 수행합니다.
                // 예: GameObject 비활성화
                resultImage = go;
                resultImage.SetActive(false);
                break;
            }
        }*/
        
        resultImage = GameObject.Find("Result_image");
        // "Result_image" 아래의 "bad" 이미지 GameObject 찾기
        if (resultImage != null) {
            timeImage = resultImage.transform.Find("timeover").gameObject;
        } else {
            Debug.LogError("Failed to find 'Result_image' GameObject.");
        }
    }

    void CookButton_Click()
    {
        if (!isMoving) // 이동 중이 아닌 경우에만 실행
        {
            if (!isCounting) // 카운트가 진행 중이 아닌 경우
            {
                StartCoroutine(StartCountdown()); // 30초 카운트 다운 시작
            }
            isMoving = true; // 이동 시작
        }
        else // 이동 중인 경우
        {
            isMoving = false; // 이동 멈춤
        }
    }

    IEnumerator StartCountdown()
    {
        isCounting = true; // 카운트가 시작됨을 표시
        int timeLeft = 5;
        while (timeLeft >= 0)
        {
            TimeCount.text = timeLeft.ToString(); // 시간을 텍스트에 표시
            yield return new WaitForSeconds(1); // 1초 대기
            timeLeft--;
        }
        isCounting = false; // 카운트가 종료됨을 표시
        cookButton.interactable = false; // CookButton 비활성화

        // "bad" 이미지 활성화
        if (timeImage != null) {
            timeImage.SetActive(true);
            Debug.Log("Found bad image: " + (timeImage != null));
        } else {
            Debug.LogError("Failed to find 'bad' image GameObject.");
        }

        // 카운트가 종료되면 MiniGame_bar와 MiniGame_ver_bar를 멈추도록 설정
        isMoving = false;
    }

    void Update()
    {
        if (isMoving)
        {
            // MiniGame_bar 이동
            float newBarX = MiniGame_bar.transform.position.x + barMoveSpeed;
            if (newBarX < -MoveRange - 2f || newBarX > MoveRange - 2f)
            {
                barMoveSpeed *= -1;
            }
            MiniGame_bar.transform.Translate(Vector3.right * barMoveSpeed);

            // MiniGame_ver_bar 이동
            float newVerBarX = MiniGame_ver_bar.transform.position.x + verBarMoveSpeed;
            if (newVerBarX < -MoveRange - 2f || newVerBarX > MoveRange - 2f)
            {
                verBarMoveSpeed *= -1;
            }
            MiniGame_ver_bar.transform.Translate(Vector3.right * verBarMoveSpeed);
        }
    }
}
