using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;

using System;
using Unity.VisualScripting;
using System.Linq;


public class DataQuest : MonoBehaviour
{
    public static DataQuest instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void InitQuestData()
    {
        QuestManager.instance.InitQuestList();
    }
    public void OnQuestDataReceived(GetUserDataResult result, List<QuestProfile> questList)
    {
        // xu li data quest List
        QuestProfile[] allQuestProfiles = Resources.LoadAll<QuestProfile>("Quests"); 
        var questListData = JsonConvert.DeserializeObject<List<QuestProfile>>(result.Data["Quest"].Value);
        questList = MapQuestProfiles(allQuestProfiles, questListData);
        // xu li data cua QuestProgress
        string questEnumData = result.Data["QuestEnum"].Value;
        string[] questEnumList = questEnumData.Split(char.Parse("/"));
        QuestProgress questProgress;
        QuestType questType;
        QuestReward questReward;
        for (int i = 0; i < questList.Count; i++)
        {
            string[] splitElement = questEnumList[i].Split(char.Parse(","));
            Enum.TryParse<QuestProgress>(splitElement[0], out questProgress);
            Enum.TryParse<QuestType>(splitElement[1], out questType);
            Enum.TryParse<QuestReward>(splitElement[2], out questReward);
            questList[i].QuestProgress = questProgress;
            questList[i].QuestType = questType;
            questList[i].QuestReward = questReward;
        }
        QuestManager.instance.UpdateQuestAccepted();
        PlayerStatsManager.instance.UpdateLevelUI();
    }

    private List<QuestProfile> MapQuestProfiles(QuestProfile[] allQuestProfiles, List<QuestProfile> questDataList)
    {
        List<QuestProfile> questProfiles = new List<QuestProfile>();

        foreach (var questData in questDataList)
        {
            // Tìm `QuestProfile` phù hợp từ tài sản
            QuestProfile questProfile = allQuestProfiles.FirstOrDefault(q => q.QuestID == questData.QuestID);
            if (questProfile != null)
            {
                //questProfile.QuestTitle = questData.QuestTitle;
                //questProfile.QuestDescription = questData.QuestDescription;
                //questProfile.QuestLevel = questData.QuestLevel; // quest theo level cua sinh vien ( 1,2,3,4)
                questProfile.QuestID = questData.QuestID;
               // questProfile.QuestSummary = questData.QuestSummary;// tom tat nv
                //questProfile.QuestPoint = questData.QuestPoint; // diem thuong
               // questProfile.QuestAnswer = questData.QuestAnswer;
                // Khởi tạo các thuộc tính khác nếu cần
                questProfiles.Add(questProfile);
            }
            else
            {
                Debug.LogWarning($"QuestProfile with ID {questData.QuestID} not found.");
            }
        }
        return questProfiles;
    }

}


