using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRecipe : MonoBehaviour
{
    public GameObject Recipe_Window_B;
    public GameObject Recipe_Window;
    // Start is called before the first frame update
    public void OnClickButton()
    {
        Recipe_Window.SetActive(true);
        ClickFish clickFish = FindObjectOfType<ClickFish>();
        clickFish.Fish_Window.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
