using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickStart : MonoBehaviour
{
    public Button Start_cook;
    public GameObject MiniGame_ver_bar; //세로 작은 바
    public GameObject MiniGame_bar; //가로 큰 바
    Vector3 pos_ver; //현재 위치
    Vector3 pos;
    float delta_ver = 3.4f;
    float delta = 3.0f;
    float speed_ver = 8.0f;
    float speed = 3.0f;
    private bool isMoving = false; // 움직임을 제어하기 위한 플래그 변수
    private float timeOffset_ver; // 세로 바를 위한 시간 오프셋
    private float timeOffset; // 가로 바를 위한 시간 오프셋 

    // Start is called before the first frame update
    void Start()
    {
        Start_cook.onClick.AddListener(OnClickButton);
        pos_ver = MiniGame_ver_bar.transform.position;
        pos = MiniGame_bar.transform.position;

        //시간 오프셋을 서로 다르게 설정
        timeOffset_ver = UnityEngine.Random.Range(0f,0f);
        timeOffset = UnityEngine.Random.Range(0f,10f);
    }
    void OnClickButton()
    {
        isMoving = !isMoving; // 버튼을 클릭하면 움직임 시작
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            //작은 세로 바 움직이기
            delta = delta_ver;
            Vector3 v = pos_ver;
            v.x += delta_ver * Mathf.Sin((Time.time+timeOffset_ver)*speed);
            MiniGame_ver_bar.transform.position = v;

            // 큰 가로 바 움직이기
            Vector3 v2 = pos;
            v2.x += delta * Mathf.Sin((Time.time+timeOffset)*speed);
            MiniGame_bar.transform.position = v2;
        }
    }
}
