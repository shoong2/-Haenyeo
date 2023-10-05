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

    [Header("알림창")]
    [SerializeField] GameObject rewardBox;
    [SerializeField] Image itemBox;
    [SerializeField] Sprite[] itemSprite;
    [SerializeField] TMP_Text itemName;
    [SerializeField] TMP_Text itemDetail;

    [SerializeField] GameObject questBox;
    [SerializeField] TMP_Text questName;
    bool isClickRewardBox = false;
    bool isClickQuestBox = false;

    bool multiCoroutine = false;

    public void ShowDialogue(Dialogue[] p_dialogues)
    {
        isDialogue = true;
        txt_Dialogue.text = "";
        txt_Name.text = "";

        dialogues = p_dialogues;
        StartCoroutine(Typewriter());
    }

    IEnumerator Typewriter()
    {
        SettingUI(true);

        string t_ReplaceText = dialogues[lineCount].contexts[contextCount];
        t_ReplaceText = t_ReplaceText.Replace("'", ",");
        t_ReplaceText = t_ReplaceText.Replace("\\n", "\n");

        //txt_Dialogue.text = t_ReplaceText;
        txt_Name.text = dialogues[lineCount].name;
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

  

        if (Input.GetMouseButtonDown(0) && !isDialogue)
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero, 0f);

            if(hit.collider !=null)
            {
                if(hit.collider.tag=="Ship")
                {
                    SceneManager.LoadScene("Room");
                }
               // Debug.Log(storage.saveData.nowIndex);
                if (SetSystemIndex(0, hit, "Won", "Room"))
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(1, 9));
                 
                }
                else if(SetSystemIndex(2,hit,"Won","Beach"))
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(10, 14));
                }
                else if(SetSystemIndex(2,hit,"Yeong", "Sea"))
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(15,15)); //장비 업그레이드
                    storage.saveData.isQuest = false;
                    storage.SaveData();
                }
                else if(SetSystemIndex(3,hit,"Won","Sea")) //수집퀘스트
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(16, 16));
                    GameManager.instance.time = 30;
                }
                else if (SetSystemIndex(4, hit, "Won", "Sea")) //수집완료
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(17, 17));
                }
                else if (SetSystemIndex(5, hit, "Won", "Sea")) //자기 퀘스트
                {
                    ShowDialogue(DatabaseManager.instance.GetDialogue(18,28));
                }
                //hit.transform.GetComponent<InteractionEvent>().GetDialogue();
                //SettingUI(true);
                //ShowDialogue(hit.transform.GetComponent<InteractionEvent>().GetDialogue());

            }
        }

        
    }

    bool SetSystemIndex(int index, RaycastHit2D hit, string character, string SceneName)
    {
        Debug.Log(hit);
        //if (storage.saveData.nowIndex == index && hit.collider.tag == character
        //    && SceneManager.GetActiveScene().name == SceneName)
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
      
        if(storage.saveData.questAllCount==0)
        {
            GetReward(0);
            quest.ActiveQuest(2);
            GameObject.FindWithTag("Won").gameObject.SetActive(false);
            //storage.saveData.questAllCount++;
        }
        else if(storage.saveData.questAllCount == 2)
        {
            storage.saveData.completeQuest += 2;
            GetReward(1);
        }
        else if(storage.saveData.nowIndex ==2)
        {
            GetReward(2);
        }
        else if (storage.saveData.nowIndex == 3)
        {
            GetReward(3);
        }
        else if (storage.saveData.nowIndex == 4)
        {
            GetReward(4);
        }
        else if (storage.saveData.nowIndex == 5)
        {
            GetReward(5);
        }
        //storage.saveData.nowIndex++;
        storage.SaveData();

        storage.SaveData();
    }



    void GetReward(int index)
    {
        if(index ==0)
        {
            StartCoroutine(ShowRewardBox(new string[] {"초보 해녀의 고무옷", "초보 해녀의 수경", "초보 해녀의 오리발"},index));
            StartCoroutine(ShowQuestBox(new string[] { "해녀복 착용하기", "바다로 가보기" }));
        }
        else if(index ==1)
        {
            isClickRewardBox = false;
            StartCoroutine(ShowRewardBox(new string[] {"초보 해녀의 칼", "초보 해녀의 호미", "초보 해녀의 작살" },index));
            StartCoroutine(ShowQuestBox(new string[] { "도구 착용하기", "바다에서 3m까지 버티기" }));
        }
        else if(index ==2)
        {
            isClickRewardBox = false;
            StartCoroutine(ShowRewardBox(new string[] { "초보 해녀의 칼", "초보 해녀의 호미", "초보 해녀의 작살" },index));
        }
        else if(index ==3)
        {
            multiCoroutine = true;
            StartCoroutine(ShowQuestBox(new string[] { "기본 재료 수집하기" }));
        }
        else if (index == 4)
        {
            multiCoroutine = true;
            StartCoroutine(ShowQuestBox(new string[] { "집으로 가서 잠자기" }));
        }
        else if (index == 5)
        {
            multiCoroutine = true;
            StartCoroutine(ShowQuestBox(new string[] { "잠수 실력을 입증하자" }));
        }

        storage.saveData.nowIndex++;

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
