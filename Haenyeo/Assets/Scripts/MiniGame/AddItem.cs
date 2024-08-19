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
    int a = 0;
    int b = 0;
    int c = 0;

    //마이너스 버튼
    public Button remove1;
    public Button remove2;
    public Button remove3;

    //InputA,B,C 데이터 정의
    int InputA=1;
    int InputB=2;
    int InputC=3;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Number1.text = "0" + "/" + InputA;
        Number2.text = "0" + "/" + InputB;
        Number3.text = "0" + "/" + InputC;
    }

    // Update is called once per frame
    public void ItemAPlus()
    {
        a++;
        Number1.text = a.ToString() + "/" + InputA;
    }
    public void ItemAMinus()
    {
        if (a>0)
        {
            a--;
            Number1.text = a.ToString() + "/" + InputA;
        }
    }
    public void ItemBPlus()
    {
        b++;
        Number2.text = b.ToString() + "/" + InputB;
    }
    public void ItemBMinus()
    {
        if (b>0)
        {
            b--;
            Number2.text = b.ToString() + "/" + InputB;
        }
    }
    public void ItemCPlus()
    {
        c++;
        Number3.text = c.ToString() + "/" + InputC;
    }
    public void ItemCMinus()
    {
        if (c>0)
        {
            c--;
            Number3.text = c.ToString() + "/" + InputC;
        }
    }
}
