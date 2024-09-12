using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddItem : MonoBehaviour
{
    //해산물 아이콘 버튼
    public GameObject FishItem1;
    public GameObject FishItem2;
    public GameObject FishItem3;

    //표시되는 텍스트
    public TMP_Text Number1;
    public TMP_Text Number2;
    public TMP_Text Number3;

    //버튼 누른 횟수 초기값 정의
    public int a = 0;
    public int b = 0;
    public int c = 0;

    //마이너스 버튼
    public Button remove1;
    public Button remove2;
    public Button remove3;

    //InputA,B,C 데이터 정의
    public int InputA=1;
    public int InputB=2;
    public int InputC=3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Number1.text = a + "/" + InputA;
        Number2.text = b + "/" + InputB;
        Number3.text = c + "/" + InputC;
    }

    // Update is called once per frame
    public void ItemAPlus()
    {
        a++;
        Number1.text = a + "/" + InputA;
    }
    public void ItemAMinus()
    {
        if (a>0)
        {
            a--;
            Number1.text = a + "/" + InputA;
        }
    }
    public void ItemBPlus()
    {
        b++;
        Number2.text = b + "/" + InputB;
    }
    public void ItemBMinus()
    {
        if (b>0)
        {
            b--;
            Number2.text = b + "/" + InputB;
        }
    }
    public void ItemCPlus()
    {
        c++;
        Number3.text = c + "/" + InputC;
    }
    public void ItemCMinus()
    {
        if (c>0)
        {
            c--;
            Number3.text = c + "/" + InputC;
        }
    }
    public void ResetItems()
    {
        a = 0;
        b = 0;
        c = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        Number1.text = a + "/" + InputA;
        Number2.text = b + "/" + InputB;
        Number3.text = c + "/" + InputC;
    }
}
