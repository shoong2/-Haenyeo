using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Beach : MonoBehaviour
{
    public GameObject[] people;

    //백하나, 강재원, 이다영
    Vector2[] position_1 = new Vector2[]
    {
        new Vector2(-5.12f, -1.11f),
        new Vector2(-1.15f, -0.01f),
        new Vector2(2.18f, -1.07f)
    };

    //백하나, 강재원, 이다영, 강민서
    Vector2[] position_2 = new Vector2[]
    {
        new Vector2(-5.87f, -0.76f),
        new Vector2(-2.63f, 0.89f),
        new Vector2(0.07f, -1.07f),
        new Vector2(3.02f, 1.19f)
    };

    //백하나, 강재원, 이다영, 강민서, 윤수연
    Vector2[] position_3 = new Vector2[]
    {
        new Vector2(-7.64f, -1.33f),
        new Vector2(-4.65f, 0.89f),
        new Vector2(-1.92f, -1.07f),
        new Vector2(0.99f, 1.19f),
        new Vector2(3.91f, -0.43f)
    };

    void Start()
    {
        for(int i=0; i<position_2.Length; i++)
        {
            people[i].gameObject.SetActive(true);
            people[i].transform.localPosition= position_2[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveSea()
    {
        SceneManager.LoadScene("Sea");
    }    
}
