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

    public bool getFishQuest = false;


    private void Awake()
    {
        playerAnim = player.GetComponent<Animator>();
        gm = FindObjectOfType<UnderSeaGameManager>();
        currentTool = FindAnyObjectByType<Tool>();
        activeToolName = tools[0].name;
        FindToolName(activeToolName);

    }

    private void Start()
    {
        for (int i = 0; i < tools.Length; i++)
        {        
            Tool emp = myTool[i];
            Button btn = tools[i].GetComponent<Button>();
            btn.onClick.AddListener(() => emp.Attack(playerAnim, gm, inven, getFishQuest));        
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
        //currentTool.SelectRay(activeToolName);
    }

    void FindToolName(string name)
    {
        for (int i = 0; i < myTool.Length; i++)
        {
            if (myTool[i].GetType().ToString() == name)
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
        //Debug.Log(currentTool.activeAttack);
    }


}
