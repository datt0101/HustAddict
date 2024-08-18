using System;
using System.Threading;
using UnityEngine;
public class ButtonManager : MonoBehaviour
{
    public static ButtonManager instance;
    public GameObject questMenu;
    public GameObject answerMenu;
    public QuestUI questUpdateMainUI;
    public GameObject ChooseQuestMenu;  
    //Handle Lag Button
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform respawnPoint;

    [SerializeField] private GameObject[] FpsButtons;
    private void Awake()
    {
        instance = this;
    }
    // Quest Menu
    public void ActiveQuestMenu()
    {
        questMenu.SetActive(true);
    }
    public void ExitQuestMenu()
    {
        questMenu.SetActive(false);
    }
    public void SwitchDailyQuest()
    {
        Debug.Log("Switch Daily Quest");
        int l = QuestManager.instance.currentQuestList.Count;
        for (int i = 0; i < l; ++i)
        {
            if (QuestManager.instance.currentQuestList[i].QuestType != QuestType.Main)
            {
                questUpdateMainUI.UpdateQuestInfo(QuestManager.instance.currentQuestList[i]);
                break;
            }
        }
    }
    public void SwitchMainQuest()
    {
        Debug.Log("Switch Main Quest ");
        questUpdateMainUI.UpdateQuestInfo(QuestManager.instance.currentQuestList[0]);
    }
    public void GiveUpQuestButton()
    {
        //QuestManager.instance.GiveUpQuest();
    }

    // Answer menu
    public void ExitAnswerMenu()
    {
        answerMenu.SetActive(false);
    }

    // Choose Quest menu
    public void ExitChooseQuestMenu()
    {
        ChooseQuestMenu.SetActive(false);
    }

    // setting menu
    public void HandleLag(GameObject map)
    {
        map.SetActive(true);
        playerTransform.position = respawnPoint.position; 
    } 

    public void ExitGame()
    {
        Debug.Log("Exit");
        Application.Quit();
    }

    public void ChooseFPS(int value)
    {
        Application.targetFrameRate = value;
    }
    public void EnableFpsButton(GameObject gameObject)
    {
        foreach(var fpsButton in FpsButtons)
        {
            fpsButton.SetActive(false);
        }
        gameObject.SetActive(true);
    }

    public void AcceptRacing(QuestProfile questProfile)
    {
        QuestManager.instance.AcceptQuest(questProfile.QuestID);
    }
}



