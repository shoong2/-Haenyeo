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

    //public Dictionary<string, int> itemCountDictionary = new Dictionary<string, int>(); // 각 아이템의 갯수를 저장할 딕셔너리
    public Button removeButton1;
    public Button removeButton2;
    public Button removeButton3;
    public GameObject fishButton; // Fish_Button 게임 오브젝트

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 아이콘들을 비활성화
        SetIconsActive(false);

        // 초기에 모든 텍스트를 0으로 설정
        UpdateItemCountText("recipe_numA", 0);
        UpdateItemCountText("recipe_numB", 0);
        UpdateItemCountText("recipe_numC", 0);

        // 해산물 버튼들에 대한 이벤트를 추가
        AddSeafoodButtonEvent("Crust_tuto_A", "Icon_a", new Vector3(-91, 211, 0), "recipe_numA");
        AddSeafoodButtonEvent("Crust_tuto_B", "Icon_b", new Vector3(81, 208, 0), "recipe_numB");
        AddSeafoodButtonEvent("Crust_tuto_C", "Icon_c", new Vector3(-91, 36, 0), "recipe_numC");

        // remove 버튼들에 대한 이벤트 추가
        AddRemoveButtonEvents(removeButton1, "recipe_numA");
        AddRemoveButtonEvents(removeButton2, "recipe_numB");
        AddRemoveButtonEvents(removeButton3, "recipe_numC");

        // Fish_Button 활성화
        fishButton.SetActive(true);
    }

    // 아이콘들을 활성화하고 위치를 설정하는 함수
    void SetIconsActive(bool active)
    {
        foreach (Transform child in iconParent)
        {
            child.gameObject.SetActive(active);
        }
    }

    void AddSeafoodButtonEvent(string buttonName, string iconName, Vector3 iconPosition, string recipeNum)
    {
        //SeafoodManagerNew.Instance.seafoodCountDict.Add(recipeNum, 0); // 초기 아이템 갯수를 0으로 설정 -> 추가가 되버림..

        Button seafoodButton = GameObject.Find(buttonName).GetComponent<Button>();
        seafoodButton.onClick.AddListener(() => AddSeafoodItem(iconName, iconPosition, recipeNum));
    }

    void AddSeafoodItem(string iconName, Vector3 iconPosition, string recipeNum) 
    {
        SeafoodManagerNew.Instance.seafoodCountDict[recipeNum]++;
         // 아이템 갯수 증가
        Debug.Log(SeafoodManagerNew.Instance.seafoodCountDict[recipeNum]);

        // 텍스트 업데이트
        UpdateItemCountText(recipeNum, SeafoodManagerNew.Instance.seafoodCountDict[recipeNum]);

        // 아이콘 활성화
         SetIconsActive(true);
    }

    // 아이템을 제거하고 갯수를 감소시키는 메서드
    private void RemoveItem(string recipeNum)
    {
        if (SeafoodManagerNew.Instance.seafoodCountDict[recipeNum] > 0)
        {
            SeafoodManagerNew.Instance.seafoodCountDict[recipeNum]--; // 아이템 갯수 감소
            UpdateItemCountText(recipeNum, SeafoodManagerNew.Instance.seafoodCountDict[recipeNum]); // 텍스트 업데이트
            Debug.Log("Item count decreased: " + SeafoodManagerNew.Instance.seafoodCountDict[recipeNum]);
        }
        else
        {
            Debug.Log("No items to remove.");
        }

        // Fish_Button 활성화
        fishButton.SetActive(true);
    }


    // 갯수를 표시하는 UI 텍스트 업데이트 함수
    void UpdateItemCountText(string recipeNum, int itemCount)
    {
        // itemCountTextPrefab에서 Text 컴포넌트를 찾아옴
        TMP_Text textComponent = itemCountTextPrefab.transform.Find(recipeNum).GetComponent<TMP_Text>();

        // 텍스트 업데이트
        if (textComponent != null)
        {
            textComponent.text = itemCount.ToString();
        }
        else
        {
            Debug.LogError("Text component not found!");
        }
    }

    // 각 remove 버튼에 대한 이벤트 추가
    public void AddRemoveButtonEvents(Button button, string recipeNum)
    {
        button.onClick.AddListener(() => RemoveItem(recipeNum));
    }
}


























