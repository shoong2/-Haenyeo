using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ClickRecipeItem : MonoBehaviour
{
    public Button RecipeItem_Button;
    public GameObject RecipeInput;
    public GameObject Recipe_NeedNumber;
    
    // Start is called before the first frame update
    void Start()
    {
        RecipeInput.SetActive(false);
        Recipe_NeedNumber.SetActive(false);
        RecipeItem_Button.onClick.AddListener(OnClickButton);
    }
    public void OnClickButton()
    {
        RecipeInput.SetActive(true);
        Recipe_NeedNumber.SetActive(true);
        
    }
}
