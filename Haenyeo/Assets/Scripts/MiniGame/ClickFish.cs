using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickFish : MonoBehaviour
{
    public GameObject Fish_Window_B;
    public GameObject Fish_Window;

    // Start is called before the first frame update
    void Start()
    {
        Fish_Window.SetActive(false);
        //GameObject.Find("Scipts/ClickRecipe").GetComponent<ClickRecipe>();
    }

    public void OnClickButton()
    {
        Fish_Window.SetActive(true);
        ClickRecipe clickRecipe = FindObjectOfType<ClickRecipe>();
        clickRecipe.Recipe_Window.SetActive(false) ;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
