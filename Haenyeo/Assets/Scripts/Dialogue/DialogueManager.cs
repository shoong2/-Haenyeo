using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class DialogueManager : MonoBehaviour
{
    [SerializeField] GameObject go_DialogueBar;
    //[SerializeField] GameObject go_DialogueNameBar;

    [SerializeField] TMP_Text txt_Dialogue;
    [SerializeField] TMP_Text txt_Name;

    Dialogue[] dialogues;

    bool isDialogue = false;
    bool isNext = false; // 특정 키 입력 대기.

    [Header("텍스트 출력 딜레이")]
    [SerializeField] float textDelay;

    int lineCount = 0; // 대화 카운트
    int contextCount = 0; // 대화 카운트

    [Header("저장소")]
    [SerializeField] SaveNLoad storage;
    //public string tagName;
    [Header("퀘스트")]
    [SerializeField] QuestManager quest;
    [Header("리워드")]
    [SerializeField] RewardManager reward;

    [Header("알림창")]
    [SerializeField] GameObject rewardBox;
    [SerializeField] Image itemBox;
    [SerializeField] Sprite[] itemSprite;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDetail;

    [SerializeField] GameObject questBox;
    [SerializeField] TMP_Text questName;

    [Header("원화")]
    [SerializeField] Image[] charImg;

    bool isClickRewardBox = false;
    bool isClickQuestBox = false;

    bool multiCoroutine = false;


    void ShowDialogueImg(string name,bool active = true)
    {

        for (int i = 0; i < charImg.Length; i++)
        {
            if (active)
            {
                if (charImg[i].name == name.ToString())
                {
                    charImg[i].gameObject.SetActive(true);
                }
                else
                    charImg[i].gameObject.SetActive(false);
            }
            else
                charImg[i].gameObject.SetActive(false);
        }


    }
    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;
        Debug.Log(dialogues.Length);
        StartCoroutine(Typewriter());
    }

    IEnumerator Typewriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        txt_Name.text = dialogues[lineCount].name[contextCount];
        ShowDialogueImg(txt_Name.text);
        for(int i=0; i<t_ReplaceText.Length; i++)
        {
            txt_Dialogue.text += t_ReplaceText[i];
            yield return new WaitForSecondsRealtime(textDelay);
        }
        isNext = true;

        
    }

    void SettingUI(bool p_flag)
    {
        go_DialogueBar.SetActive(p_flag);
        //go_DialogueNameBar.SetActive(p_flag);
    }

    private void Update()
    {

        if (isDialogue)
        {
            if (isNext)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    isNext = false;
                    txt_Dialogue.text = "";
                    if(++contextCount < dialogues[lineCount].contexts.Length)
                    {
                        StartCoroutine(Typewriter());
                    }
                    else
                    {
                        contextCount = 0;
                        if(++lineCount<dialogues.Length)
                        {
                            StartCoroutine(Typewriter());
                        }
                        else
                        {
                            EndDialogue();
                        }
                    }
                }
            }
        }

  

        if (Input.GetMouseButtonDown(0) && !isDialogue) //대화 클릭
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f, LayerMask.GetMask("NPC"));

            if(hit)
            {
                ShowDialogue(DatabaseManager.instance.GetDialogue(storage.saveData.nowIndex));
            }
        }

        
    }

    bool SetSystemIndex(int index, RaycastHit2D hit, string character, string SceneName)
    {
        if (storage.saveData.questAllCount == index && hit.collider.tag == character
            && SceneManager.GetActiveScene().name == SceneName)
        {
            return true;
        }
        else
            return false;
    }

    public void EndDialogue()
    {
        isDialogue = false;
        contextCount = 0;
        lineCount = 0;
        dialogues = null;
        isNext = false;
        SettingUI(false);
        StopAllCoroutines();
        ShowDialogueImg("stop", false);

       // reward.GetReward(storage.saveData.nowIndex);
        quest.Active(storage.saveData.nowIndex);
        

        //if(storage.saveData.questAllCount==0)
        //{
        //    reward.GetReward(0);
        //    quest.Active(0);
        //    GameObject.FindWithTag("Won").gameObject.SetActive(false);
        //    //storage.saveData.questAllCount++;
        //}

    }




    IEnumerator ShowRewardBox(string[] name, int index)
    {
        for(int i=0; i<name.Length; i++)
        {
            Debug.Log(name[i]);
            Debug.Log(i);
            itemName.text = name[i];
            if((index == 0) ||index ==1)
            {
                Debug.Log("ddd");
                if(index==0)
                    itemBox.sprite = itemSprite[i];
                else
                    itemBox.sprite = itemSprite[3+i];
                itemDetail.text = "'" + name[i] + "'" + " 을 획득했습니다!";
            }
            else if(index == 2)
            {
                itemBox.sprite = itemSprite[3+i];
                itemDetail.text = "'" + name[i] + "'" + " 의\n능력치가 상승했습니다!";
            }
            else
                itemDetail.text = "'" + name[i] + "'" + " 을 획득했습니다!";
            rewardBox.SetActive(true);

            if (i != name.Length - 1)
                yield return new WaitUntil(() => isClickRewardBox);
            
            isClickRewardBox = false;
        }
        multiCoroutine = true;
        yield return null;
    }

    IEnumerator ShowQuestBox(string[] quest)
    {
        yield return new WaitUntil(() => multiCoroutine && isClickRewardBox);
        for (int i = 0; i < quest.Length; i++)
        {
            questName.text = quest[i];
            questBox.SetActive(true);
            if (i != quest.Length - 1)
                yield return new WaitUntil(() => isClickQuestBox);
            isClickQuestBox = false;
        }
        multiCoroutine = false;
    }

    public void ClickRewardBox()
    {
        isClickRewardBox = true;
        rewardBox.SetActive(false);

    }

    public void ClickQuestBox()
    {
        isClickQuestBox = true;
        questBox.SetActive(false);
    }    
}
