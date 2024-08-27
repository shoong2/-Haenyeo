using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info_Eon : MonoBehaviour
{
   // private TMP_Text infoTextA;
   // private TMP_Text infoTextB;
   // private TMP_Text infoTextC;
   //infoText를 하나씩 찾는 것보다 아래 배열 하나에 넣는 것이 더 효율성 있음 start() 에서 넣어줌 
    TMP_Text[] infoText;

    //Transform infoRTransform;
    public Transform infoRTransform; // 굳이 Find를 쓸 필요 없음 

    private bool showInfo = true;
    public GameObject recipeInfo;

    private void Awake()
    {
        //Transform canvasTransform = GameObject.Find("Canvas2").transform;
        //위에서 이미 infoRTransform를 public으로 찾아줬기 때문에 굳이 캔버스를 찾을 이유가 없음

        //if (canvasTransform != null)
        //{
        //    infoRTransform = canvasTransform.Find("infoR");
        //    if (infoRTransform != null)
        //    {
        //        infoTextA = infoRTransform.Find("info_numA")?.GetComponent<TMP_Text>();
        //        infoTextB = infoRTransform.Find("info_numB")?.GetComponent<TMP_Text>();
        //        infoTextC = infoRTransform.Find("info_numC")?.GetComponent<TMP_Text>();

        //        if (infoTextA == null || infoTextB == null || infoTextC == null)
        //        {
        //            Debug.LogError("One or more info texts not found");
        //        }
        //    }
        //    else
        //    {
        //        Debug.LogError("infoR not found under Canvas2");
        //    }
        //}
        //else
        //{
        //    Debug.LogError("Canvas2 not found");
        //}
    }

    void Start()
    {
        infoText = infoRTransform.GetComponentsInChildren<TMP_Text>();
        UpdateInfoText();
    }

    public void UpdateInfoText()
    {
        if (showInfo)
        {
            string combinedInfoA = "/" + SeafoodManagerNew.Instance.GetSeafoodCount("seafood1");
            string combinedInfoB = "/" + SeafoodManagerNew.Instance.GetSeafoodCount("seafood2");
            string combinedInfoC = "/" + SeafoodManagerNew.Instance.GetSeafoodCount("seafood3");

            UpdateInfoText(combinedInfoA, combinedInfoB, combinedInfoC);
            Debug.Log("UpdateInfoText executed successfully");
        }
        else
        {
            HideInfoText();
        }
    }

    public void UpdateInfoText(string combinedInfoA, string combinedInfoB, string combinedInfoC)
    {
        infoText[0].text = combinedInfoA;
        infoText[1].text = combinedInfoB;
        infoText[2].text = combinedInfoC;
        //if (infoTextA != null && infoTextB != null && infoTextC != null)
        //{
        //    infoTextA.text = combinedInfoA;
        //    infoTextB.text = combinedInfoB;
        //    infoTextC.text = combinedInfoC;
        //    Debug.Log("Info texts updated");
        //}
        //else
        //{
        //    Debug.LogError("One or more info texts are null");
        //}
    }

    public void HideInfoText()
    {
        recipeInfo.SetActive(false);
        infoRTransform.gameObject.SetActive(false);
     

        SeafoodManagerNew.Instance.ResetRecipeCounts();
        Debug.Log("Recipe counts reset to 0");

        showInfo = false;
    }

    public void ShowInfo()
    {
        showInfo = true;
        UpdateInfoText();
    }
}
