using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchButton : MonoBehaviour
{
    public GameObject recipeImage; // Recipe 이미지를 표시할 GameObject
    public GameObject ingredientWindow; // 재료 창을 표시할 GameObject

    // Start is called before the first frame update
    void Start()
    {
        // 초기에는 두 UI 요소를 비활성화
        recipeImage.SetActive(true);
        ingredientWindow.SetActive(false);
        
        // 버튼 A와 B에 대한 클릭 이벤트 추가
        Button buttonA = GameObject.Find("MiniGame_RecipeB").GetComponent<Button>();
        Button buttonB = GameObject.Find("MiniGame_FishB").GetComponent<Button>();

        buttonA.onClick.AddListener(ShowRecipeImage);
        buttonB.onClick.AddListener(ShowIngredientWindow);
    }

    // 버튼 A를 누르면 호출되는 함수
    void ShowRecipeImage()
    {
        recipeImage.SetActive(true);
        ingredientWindow.SetActive(false);
    }

    // 버튼 B를 누르면 호출되는 함수
    void ShowIngredientWindow()
    {
        recipeImage.SetActive(false);
        ingredientWindow.SetActive(true);
    }
}