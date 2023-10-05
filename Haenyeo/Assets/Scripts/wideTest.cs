using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wideTest : MonoBehaviour
{
    int p_width = Screen.width;
    int p_height = Screen.height;

    float height;
    float width;

    RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        height = 2 * Camera.main.orthographicSize;
        width = height * Camera.main.aspect;
        rect = GetComponent<RectTransform>();
        rect.sizeDelta = new Vector2(rect.sizeDelta.x * height /10, rect.sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
