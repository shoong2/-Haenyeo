using UnityEngine;
using UnityEngine.UI;

public class RecipeItem : MonoBehaviour
{
    private Button recipeButton;
    private Info infoScript;
    private Button cookButton;

    void Start()
    {
        recipeButton = GetComponent<Button>();
        recipeButton.onClick.AddListener(OnRecipeButtonClick);
        infoScript = FindObjectOfType<Info>();
        cookButton = GameObject.Find("MiniGame_CookButton").GetComponent<Button>();
    }

    void OnRecipeButtonClick()
    {
        if (infoScript != null)
        {
            infoScript.ShowInfo();
        }

        if (cookButton != null)
        {
            cookButton.interactable = true;
            Debug.Log("MiniGame_CookButton enabled");
        }
        else
        {
            Debug.LogError("MiniGame_CookButton not found");
        }
    }
}