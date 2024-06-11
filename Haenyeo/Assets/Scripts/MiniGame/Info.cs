using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    private TMP_Text infoTextA;
    private TMP_Text infoTextB;
    private TMP_Text infoTextC;

    private void Awake()
    {
        Transform canvasTransform = GameObject.Find("Canvas2").transform;
        infoTextA = canvasTransform.Find("infoR/info_numA").GetComponent<TMP_Text>();
        infoTextB = canvasTransform.Find("infoR/info_numB").GetComponent<TMP_Text>();
        infoTextC = canvasTransform.Find("infoR/info_numC").GetComponent<TMP_Text>();
        Debug.Log(infoTextA);
    }

    void Start()
    {
        // 정보 텍스트 업데이트
        UpdateInfoText();
    }

    // 정보를 업데이트하는 함수
    public void UpdateInfoText()
    {
        // 각 해산물에 대한 정보를 가져와 "/"와 함께 출력합니다.
        string combinedInfoA = "/" + SeafoodManagerNew.Instance.GetSeafoodCount("seafood1");
        Debug.Log(SeafoodManagerNew.Instance.GetSeafoodCount("seafood1"));
        string combinedInfoB = "/" + SeafoodManagerNew.Instance.GetSeafoodCount("seafood2");
        string combinedInfoC = "/" + SeafoodManagerNew.Instance.GetSeafoodCount("seafood3");

        // 정보 텍스트를 업데이트합니다.
        UpdateInfoText(combinedInfoA, combinedInfoB, combinedInfoC);
        Debug.Log("이것이 출력된다면 UpdateInfoText는 정상");
    }

    // 정보를 업데이트하는 함수
    public void UpdateInfoText(string combinedInfoA, string combinedInfoB, string combinedInfoC)
    {
        // 각각의 info_numA, info_numB, info_numC 텍스트 오브젝트에 문자열을 설정합니다.
        if (infoTextA != null && infoTextB != null && infoTextC != null)
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


