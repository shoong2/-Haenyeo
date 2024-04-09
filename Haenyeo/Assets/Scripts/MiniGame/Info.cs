using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Info : MonoBehaviour
{
    private TMP_Text infoTextA;
    private TMP_Text infoTextB;
    private TMP_Text infoTextC;

    private int numA = 1; // seafood1의 갯수
    private int numB = 2; // seafood2의 갯수
    private int numC = 3; // seafood3의 갯수

    // Start is called before the first frame update
    private void Awake() {
        Transform canvasTransform = GameObject.Find("Canvas").transform;
        infoTextA = canvasTransform.Find("infoR/info_numA").GetComponent<TMP_Text>();
        infoTextB = canvasTransform.Find("infoR/info_numB").GetComponent<TMP_Text>();
        infoTextC = canvasTransform.Find("infoR/info_numC").GetComponent<TMP_Text>();
    }
    void Start()
    {
        // Canvas 오브젝트를 시작으로 하위 오브젝트 찾기


        // 정보 텍스트 업데이트
        UpdateInfoText();
    }

    // 정보를 업데이트하는 함수
    public void UpdateInfoText()
    {
        // 각 해산물에 대한 정보를 가져와 "/"와 함께 출력합니다.
        string combinedInfoA = "/" + numA;
        string combinedInfoB = "/" + numB;
        string combinedInfoC = "/" + numC;

        // 정보 텍스트를 업데이트합니다.
        UpdateInfoText(combinedInfoA, combinedInfoB, combinedInfoC);
        Debug.Log("이것이 출력된다면 UpdateInfoText는 정상");
    }

    // 정보를 업데이트하는 함수
    public void UpdateInfoText(string combinedInfoA, string combinedInfoB, string combinedInfoC)
    {
        // 각각의 info_numA, info_numB, info_numC 텍스트 오브젝트에 문자열을 설정합니다.
        if(infoTextA != null && infoTextB != null && infoTextC != null)
        {
            infoTextA.text = combinedInfoA;
            infoTextB.text = combinedInfoB;
            infoTextC.text = combinedInfoC;
            Debug.Log("hi");
        }
        else
        {
            Debug.LogError("infoTextA, infoTextB, infoTextC 중 하나 이상이 null입니다.");
        }
    }
}
