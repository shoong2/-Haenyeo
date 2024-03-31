using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_cook : MonoBehaviour
{
    public GameObject MiniGame_bar;
    public GameObject MiniGame_ver_bar;

    private const float MoveRange = 3.5f; // 이동 범위

    public float barMoveSpeed = 0.05f;   // MiniGame_bar의 이동 속도
    public float verBarMoveSpeed = 0.01f; // MiniGame_ver_bar의 이동 속도

    private bool isMoving = false;

    private Vector3 barStartPosition;
    private Vector3 verBarStartPosition;

    void Start()
    {
        // CookButton에 클릭 이벤트 핸들러 추가
        GameObject cookButton = GameObject.Find("MiniGame_CookButton");
        cookButton.GetComponent<Button>().onClick.AddListener(CookButton_Click);

        // 시작 위치 저장
        barStartPosition = MiniGame_bar.transform.position;
        verBarStartPosition = MiniGame_ver_bar.transform.position;
    }

    void CookButton_Click()
    {
        isMoving = !isMoving; // 이동 상태 변경
    }

    void Update()
    {
        if (isMoving)
        {
            // MiniGame_bar 이동
            float newBarX = MiniGame_bar.transform.position.x + barMoveSpeed;
            if (newBarX < -MoveRange-2f || newBarX > MoveRange-2f)
            {
                barMoveSpeed *= -1;
            }
            MiniGame_bar.transform.Translate(Vector3.right * barMoveSpeed);

            // MiniGame_ver_bar 이동
            float newVerBarX = MiniGame_ver_bar.transform.position.x + verBarMoveSpeed;
            if (newVerBarX < -MoveRange-2f || newVerBarX > MoveRange-2f)
            {
                verBarMoveSpeed *= -1;
            }
            MiniGame_ver_bar.transform.Translate(Vector3.right * verBarMoveSpeed);
        }
    }
}
