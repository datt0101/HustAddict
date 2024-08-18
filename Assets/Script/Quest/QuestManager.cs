using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    public List<QuestProfile> questList;
    public List<QuestProfile> currentQuestList;
    public QuestUI questUI;
    public QuestLogButton[] questLogButton; // danh sách các button chứa tên nhiệm vụ trong UI, luôn có sẵn 11 phần tử tuong ứng vs tối đa 11 nv được nhận
                                            // button nào có QuestProfile khác null thì sẽ bật lên, còn ko sẽ luôn ở trạng thái tắt
                                            // khi có sự thay đổi ở currentQuestList là phải thay đổi tương tự ở danh sách QuestLogButton[];
    public int questIDLogButton = 0;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        AutoEnterIdOfQuest();
        //UpdateQuestAccepted();
    }
    public void InitQuestList()
    {
        int flag = 0;
        Debug.Log("INIT");
        int l = questList.Count;
        for (int i = 0; i < l; ++i)
        {
            if (questList[i].QuestType == QuestType.Main && flag == 0)
            {
                questList[i].QuestProgress = QuestProgress.Accepted;
                flag = 1;
            }
            else
                questList[i].QuestProgress = QuestProgress.Available;
        }

        UpdateQuestAccepted();
    }
    public void AutoEnterIdOfQuest()
    {
        int l = questList.Count;
        for (int i = 0; i < l; i++)
        {
            questList[i].QuestID = i;
        }
    }
    public void UpdateQuestAccepted() // update lai nhung nhiem vu da nhan
    {
        int l = questList.Count;
        int index = 0;
        for (int i = 0; i < l; i++)
        {
            if (questList[i].QuestProgress == QuestProgress.Accepted)
            {
                Debug.Log("load quest");
                currentQuestList.Add(questList[i]); // cap nhat danh sach nhiem vu hien tai
                questLogButton[index].UpdateDataQuestLogButton(questList[i]); // cap nhat danh sach cac button nhiem vu trong UI
                index++;
            }
        }
        if (currentQuestList.Count > 0)
        {
            questUI.UpdateQuestInfo(currentQuestList[0]);
        }
    }
    public void ResetDailyQuest()
    {
        Debug.Log("Reset");
        int l = questList.Count;
        for (int i = 0; i < l; ++i)
        {
            if (questList[i].QuestType != QuestType.Main && (questList[i].QuestProgress == QuestProgress.Accepted || questList[i].QuestProgress == QuestProgress.Complete))
            {
                questList[i].QuestProgress = QuestProgress.Available;
            }
        }
        for (int i = 1; i < questLogButton.Length; ++i)
        {
            questLogButton[i].EmptyDataQuestLogButton();
        }
        for (int i = 1; i < currentQuestList.Count; ++i)
        {
            currentQuestList.Remove(currentQuestList[i]);
        }

        questUI.UpdateQuestInfo(currentQuestList[0]);
        PlayFabData.instance.SaveData();
    }
    public void AcceptQuest(int questID)
    {
        if (currentQuestList.Count > 11)
        {
            return;
        }
        int l = questList.Count;
        for (int i = 0; i < l; i++)
        {
            if (questList[i].QuestID == questID && questList[i].QuestProgress == QuestProgress.Available)
            {
                // truong hop la nhiem vu chinh thi chen vao` dau`
                if (questList[i].QuestType == QuestType.Main)
                {
                    Debug.Log("AcceptMainQuest: " + questList[i].QuestTitle);
                    currentQuestList.Insert(0, questList[i]); // nhiem vu chinh tuyen luon luon o dau current List
                    questList[i].QuestProgress = QuestProgress.Accepted;
                    questUI.UpdateQuestInfo(currentQuestList[0]); // cap nhat UI
                    questUI.ShowAssistantUI(); // show ra Assistant
                    questLogButton[0].UpdateDataQuestLogButton(currentQuestList[0]); // cap nhat danh sach button nhiem vu
                }
                else
                {
                    Debug.Log("AcceptQuest");
                    currentQuestList.Add(questList[i]); // thêm vào danh sách currentList ở vị trí cuối cùng -> danh sách questLogButton cũng cần cập nhật ở vị trí tương ứng
                    questList[i].QuestProgress = QuestProgress.Accepted;
                    int lengthOfCurrentList = currentQuestList.Count;
                    questUI.UpdateQuestInfo(currentQuestList[lengthOfCurrentList - 1]); // UI luôn show ra nhiệm vụ nhận gần nhất
                    questUI.ShowAssistantUI();
                    questLogButton[lengthOfCurrentList - 1].UpdateDataQuestLogButton(questList[i]); //cập nhật QuestProfile vào nút ở phần tử cuối cùng 
                }
                AudioManager.instance.PlayNotifySound();
                PlayFabData.instance.SaveData();
                break;
            }
        }
    }
    public void GiveUpQuest()
    {
        Debug.Log("ID: " + questIDLogButton);
        
        // bo nv ra khoi danh sach hien tai
        int l = currentQuestList.Count;
        for (int i = 0; i < l; i++)
        {
            if (currentQuestList[i].QuestID == questIDLogButton && currentQuestList[i].QuestProgress == QuestProgress.Accepted)
            {
                Debug.Log("Type: " + currentQuestList[i].QuestType);
                if (currentQuestList[i].QuestType == QuestType.Main)
                {
                    Debug.Log("Cant Give Up");
                }
                else
                {
                    Debug.Log("GiveUpQuest");
                    currentQuestList[i].QuestProgress = QuestProgress.Available;
                    currentQuestList.Remove(currentQuestList[i]); // bỏ nhiệm vụ thứ i
                    questLogButton[i].EmptyDataQuestLogButton(); // cũng phải bỏ ở vị trí thứ i tương ứng
                    questUI.UpdateQuestInfo(null);
                    SortQuestLogButton();
                    AudioManager.instance.PlayGiveUpSound();
                }
            }
        }
        PlayFabData.instance.SaveData();
    }
    public void CompleteQuest(int questID)
    {
        int l = currentQuestList.Count;
        for (int i = 0; i < l; i++)
        {
            if (currentQuestList[i].QuestID == questID && currentQuestList[i].QuestProgress == QuestProgress.Accepted)
            {
                ButtonManager.instance.ExitQuestMenu();
                questUI.CompleteQuestEffect();
                Debug.Log("CompleteQuest: " + i);
                currentQuestList[i].QuestProgress = QuestProgress.Complete;
                PlayerStatsManager.instance.AddStats(currentQuestList[i]);
                AudioManager.instance.PlayCompleteQuestSound();
                if (currentQuestList[i].QuestType == QuestType.Main)
                {
                    currentQuestList.Remove(currentQuestList[i]);
                    AcceptQuest(FindIdOfMainQuest());
                    
                }
                else
                {
                    if (currentQuestList[i].QuestType == QuestType.Study)
                    {
                        Debug.Log("Complete Study Quest");
                        TimeManager.instance.AddHour(2); // nhiệm vụ study mất 2 tiếng để hoàn thành
                    }
                    currentQuestList.Remove(currentQuestList[i]);
                    questLogButton[i].EmptyDataQuestLogButton(); // remove o vi tri i
                    SortQuestLogButton();
                }
                PlayFabData.instance.SaveData();
                break;
            }
        }
        if (currentQuestList.Count > 0)
        {
            questUI.UpdateQuestInfo(currentQuestList[0]);
        }
    }
    public int FindIdOfMainQuest()
    {
        int l = questList.Count;
        for (int i = 0; i < l; ++i)
        {
            if (questList[i].QuestProgress == QuestProgress.Available && questList[i].QuestType == QuestType.Main)
                return questList[i].QuestID;
        }
        return 0;
    }
    public void SortQuestLogButton()
    {
        int l = questLogButton.Length;
        for (int i = 0; i < l - 1; ++i)
        {
            if (questLogButton[i].questProfile == null)
            {
                questLogButton[i].UpdateDataQuestLogButton(questLogButton[i + 1].questProfile);
                questLogButton[i + 1].EmptyDataQuestLogButton();
            }
        }
    }

    public bool isQuestAccepted(int questID)
    {
        int l = currentQuestList.Count;
        {
            for (int i = 0; i < l; ++i)
            {
                if (currentQuestList[i].QuestID == questID)
                    return true;
            }
        }

        return false;
    }

}

