using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    [SerializeField] Image[] tools;

    public GameObject player;
    Animator playerAnim;

    private void Start()
    {
        playerAnim = player.GetComponent<Animator>();
    }

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

    public void Hoe()
    {
        playerAnim.SetTrigger("Hoe");
        //click = true;
    }

    public void Knife()
    {
        playerAnim.SetTrigger("Knife");
        //click = true;
    }

    public void Pole()
    {
        playerAnim.SetTrigger("Pole");
        //click = true;
        //playerAnim.SetBool("test", true);
    }
}
