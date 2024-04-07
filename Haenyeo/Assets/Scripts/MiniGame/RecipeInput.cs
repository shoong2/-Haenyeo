 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeInput : MonoBehaviour
{
    public GameObject iconPrefab; // 아이콘의 프리팹
    public Transform iconParent; // 아이콘들을 배치할 부모 객체
    public GameObject itemCountTextPrefab; // 갯수를 표시할 UI 텍스트의 프리팹
    public Transform textParent; // 텍스트들을 배치할 부모 객체

    private int itemCountA = 0; // 해산물 버튼 A를 누른 횟수
    private int itemCountB = 0; // 해산물 버튼 B를 누른 횟수
    private int itemCountC = 0; // 해산물 버튼 C를 누른 횟수

    private bool iconsActivated = false; // 아이콘이 활성화되었는지 여부를 나타내는 플래그

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 아이콘들을 비활성화
        SetIconsActive(false);

        // 해산물 버튼들에 대한 이벤트를 추가
        AddSeafoodButtonEvent("Crust_tuto_A", "Icon_a", new Vector3(-91, 211, 0), "Recipe_numA");
        AddSeafoodButtonEvent("Crust_tuto_B", "Icon_b", new Vector3(81, 208, 0), "Recipe_numB");
        AddSeafoodButtonEvent("Crust_tuto_C", "Icon_c", new Vector3(-91, 36, 0), "Recipe_numC");
    }

    // 아이콘들을 활성화하고 위치를 설정하는 함수
    void SetIconsActive(bool active)
    {
        for (int i = 0; i < iconParent.childCount; i++)
        {
            GameObject icon = iconParent.GetChild(i).gameObject;
            icon.SetActive(active);

            // 아이콘의 재질 색상의 알파 값을 1.0으로 설정하여 완전히 불투명하게 만듭니다.
            if (active)
            {
                var iconRenderer = icon.GetComponent<Renderer>();
                if (iconRenderer != null)
                {
                    Color iconColor = iconRenderer.material.color;
                    iconColor.a = 1.0f;
                    iconRenderer.material.color = iconColor;
                }
            }
        }
    }

    void AddSeafoodButtonEvent(string buttonName, string iconName, Vector3 iconPosition, string recipeNum)
    {
        int itemCount = 0; // 초기값 설정

        UpdateItemCountText(itemCount, recipeNum);

        Button seafoodButton = GameObject.Find(buttonName).GetComponent<Button>();
        seafoodButton.onClick.AddListener(() => AddSeafoodItem(iconName, iconPosition, ref itemCount, recipeNum));
    }

    void AddSeafoodItem(string iconName, Vector3 iconPosition, ref int itemCount, string recipeNum) 
    {
       // Debug.Log("Click");

        // 갯수 증가
        itemCount++;

        // 텍스트 업데이트
        UpdateItemCountText(itemCount, recipeNum);

        // 아이콘 생성 및 활성화
        // CreateIcon(iconName, iconPosition);
        // SetIconsActive(true);

        // 아이콘이 아직 활성화되지 않았는지 확인
        if (!iconsActivated)
        {
            SetIconsActive(true);
            iconsActivated = true;
        }
    }

    // 갯수를 표시하는 UI 텍스트 업데이트 함수
    void UpdateItemCountText(int itemCount, string recipeNum)
    {
        // itemCountTextPrefab에서 Text 컴포넌트를 찾아옴
        TMP_Text textComponent = itemCountTextPrefab.transform.Find(recipeNum).GetComponent<TMP_Text>();

        // 텍스트 업데이트
        if (textComponent != null)
        {
            textComponent.text = itemCount.ToString();
            // 텍스트 위치 설정
            // textComponent.rectTransform.localPosition = position;
        }
        else
        {
            Debug.LogError("Text component not found!");
        }
    }
}