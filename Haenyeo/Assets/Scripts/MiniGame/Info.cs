using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Info : MonoBehaviour
{
    private TMP_Text infoTextA;
    private TMP_Text infoTextB;
    private TMP_Text infoTextC;
    private bool showInfo = true;

    private void Awake()
    {
        Transform canvasTransform = GameObject.Find("Canvas2").transform;
        if (canvasTransform != null)
        {
            Transform infoRTransform = canvasTransform.Find("infoR");
            if (infoRTransform != null)
            {
                infoTextA = infoRTransform.Find("info_numA")?.GetComponent<TMP_Text>();
                infoTextB = infoRTransform.Find("info_numB")?.GetComponent<TMP_Text>();
                infoTextC = infoRTransform.Find("info_numC")?.GetComponent<TMP_Text>();

                if (infoTextA == null || infoTextB == null || infoTextC == null)
                {
                    Debug.LogError("One or more info texts not found");
                }
            }
            else
            {
                Debug.LogError("infoR not found under Canvas2");
            }
        }
        else
        {
            Debug.LogError("Canvas2 not found");
        }
    }

    void Start()
    {
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
        if (infoTextA != null && infoTextB != null && infoTextC != null)
        {
            infoTextA.text = combinedInfoA;
            infoTextB.text = combinedInfoB;
            infoTextC.text = combinedInfoC;
            Debug.Log("Info texts updated");
        }
        else
        {
            Debug.LogError("One or more info texts are null");
        }
    }

    public void HideInfoText()
    {
        if (infoTextA != null && infoTextB != null && infoTextC != null)
        {
            infoTextA.text = "";
            infoTextB.text = "";
            infoTextC.text = "";
            Debug.Log("Info texts hidden");
        }
        else
        {
            Debug.LogError("One or more info texts are null");
        }

        // Reset recipe counts in SeafoodManagerNew
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
