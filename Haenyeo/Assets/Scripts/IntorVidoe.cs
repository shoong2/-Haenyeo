using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
public class IntorVidoe : MonoBehaviour
{
    public VideoPlayer intro;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (intro.time >2f && !intro.isPlaying)
        {
            SceneManager.LoadScene("Title");
        }
    }
}
