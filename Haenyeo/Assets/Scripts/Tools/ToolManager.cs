using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolManager : MonoBehaviour
{
    [Header("Tool UI")]
    public Image[] tools;
    public GameObject[] toolGuide; //툴 가이드 자식
    public GameObject toolGuideGroup; //툴가이드 부모객체
    Button[] toolButton;

    [Header("Tools")]
    public Tool[] myTool; //툴
    Tool currentTool; // 현재 툴
    public string activeToolName; //활성화 된 툴 이름

    [Header("Player")]
    public Player player;
    Animator playerAnim;
    protected Vector3 dir;

    public UnderSeaGameManager gm;
    public Inventory inven;

    private void Start()
    {
        playerAnim = player.GetComponent<Animator>();
        gm = FindObjectOfType<UnderSeaGameManager>();
        activeToolName = tools[0].name;
        FindToolName(activeToolName);

        for (int i = 0; i < tools.Length; i++)
        {

        }
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
                    activeToolName = tools[0].name;
                    break;
                }
                tools[i + 1].gameObject.SetActive(true);
                activeToolName = tools[i+1].name;
                break;
            }
            else
                tools[i].gameObject.SetActive(false);
                          
        }
        FindToolName(activeToolName);
    }

    void FindToolName(string name)
    {
        for (int i = 0; i < myTool.Length; i++)
        {
            if (myTool[i].name == name)
            {
                myTool[i].Init();
                currentTool = myTool[i];
            }
        }
    }

    private void Update()
    {
        dir = player.render.flipX ? Vector3.right : Vector3.left;
        currentTool.SetRaycast(dir, player);
        currentTool.GetRaycast(toolGuideGroup, player, toolGuide);
    }

    //public void Hoe()
    //{
    //    playerAnim.SetTrigger("Hoe");
    //    //click = true;
    //}

    //public void Knife()
    //{
    //    playerAnim.SetTrigger("Knife");
    //    //click = true;
    //}

    //public void Pole()
    //{
    //    playerAnim.SetTrigger("Pole");
    //    //click = true;
    //    //playerAnim.SetBool("test", true);
    //}
}
