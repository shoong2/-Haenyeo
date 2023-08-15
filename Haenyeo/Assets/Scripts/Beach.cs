using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Beach : MonoBehaviour
{
    public GameObject[] peole;

    void Start()
    {
        
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
