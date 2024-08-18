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


public class DataAchievement: MonoBehaviour
{
    public static DataAchievement instance;
    
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void InitAchievementData()
    {
        AchievementManager.instance.InitAchievementList();
    }
    public void OnAchievementDataReceived(GetUserDataResult result, List<AchievementProfile> achievementList)
    {
        // xu li data quest List
        AchievementProfile[] allAchievementProfiles = Resources.LoadAll<AchievementProfile>("Achievements"); ;
        var achievementListData = JsonConvert.DeserializeObject<List<AchievementProfile>>(result.Data["Achievement"].Value);
        achievementList = MapAchievementProfiles(allAchievementProfiles, achievementListData);

        //for (int i = 0;i<achievementList.Count;i++)
        //{
        //    Debug.Log("After Map" + achievementList[i].AchievementStatus);
        //}
        AchievementManager.instance.UploadUnlockAchievement();
    }
    private List<AchievementProfile> MapAchievementProfiles(AchievementProfile[] allAchievementProfiles, List<AchievementProfile> achievementDataList)
    {
        List<AchievementProfile> achievementProfiles = new List<AchievementProfile>();

        foreach (var achievementData in achievementDataList)
        {
            // Tìm `QuestProfile` phù hợp từ tài sản
            AchievementProfile achievementProfile = allAchievementProfiles.FirstOrDefault(q => q.AchievementID == achievementData.AchievementID);
            if (achievementProfile != null)
            {
                achievementProfile.AchievementID = achievementData.AchievementID;
                //achievementProfile.AchievementName = achievementData.AchievementName;
                //achievementProfile.AchievementDescription = achievementData.AchievementDescription;
                //achievementProfile.AchievementReward = achievementData.AchievementReward;
                achievementProfile.AchievementStatus = achievementData.AchievementStatus;
                // Khởi tạo các thuộc tính khác nếu cần
                achievementProfiles.Add(achievementProfile);
            }
            else
            {
                Debug.LogWarning($"AchievementProfile with ID {achievementData.AchievementID} not found.");
            }
        }
        return achievementProfiles;
    }
}


