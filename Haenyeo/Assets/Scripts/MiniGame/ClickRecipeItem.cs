using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
//private Button btn;

public class ClickRecipeItem : MonoBehaviour
{
    public Button RecipeItem_Button;
    public GameObject RecipeInput;
    public GameObject Recipe_NeedNumber;
    //public ClickStart clickStartscript;
    public Button btn;
    
    // Start is called before the first frame update
    void Start()
    {
        RecipeInput.SetActive(false);
        Recipe_NeedNumber.SetActive(false);
        RecipeItem_Button.onClick.AddListener(OnClickButton);
        ClickStart clickStart = FindObjectOfType<ClickStart>();
        btn = clickStart.Start_cook;
        btn.interactable = false;


    }
    public void OnClickButton()
    {
        RecipeInput.SetActive(true);
        Recipe_NeedNumber.SetActive(true);
        ClickStart clickStart = FindObjectOfType<ClickStart>();
        btn = clickStart.Start_cook;
        btn.interactable = true;
        
    }
}
