using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    [SerializeField] Image[] tools;

    public void ChangeTool()
    {
        for(int i=0; i< tools.Length; i++)
        {
            if (tools[i].gameObject.activeSelf == true)
            {
                tools[i].gameObject.SetActive(false);
                if (i == tools.Length - 1)
                {
                    tools[0].gameObject.SetActive(true);
                    break;
                }
                tools[i + 1].gameObject.SetActive(true);
                break;
            }
            else
                tools[i].gameObject.SetActive(false);
        }
    }
}
