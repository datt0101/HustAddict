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


public class DataHandler : MonoBehaviour
{
    [SerializeField] private DataQuest dataQuest;
    [SerializeField] private DataTime dataTime;
    [SerializeField] private DataUserProfile dataUserProfile;
    [SerializeField] private DataAchievement dataAchievement;
    [SerializeField] private DataShop dataShop;
    public void ReceiveData(GetUserDataResult result, List<QuestProfile> questList, List<AchievementProfile> achievementList, PlayerProfile playerProfile,List<ItemProfile> itemList)
    {
        PlayFabAuth.instance.GetTitleData();
        dataUserProfile.OnPlayerDataReceived(result, playerProfile);
        dataQuest.OnQuestDataReceived(result, questList);
        dataAchievement.OnAchievementDataReceived(result, achievementList);
        dataTime.OnTimeDataReceived(result);
        dataShop.OnShopDataReceived(result, itemList);
    }

    public void InitData()
    {
        PlayFabAuth.instance.GetTitleData();
        dataQuest.InitQuestData();
        dataTime.InitTimeData();
        dataAchievement.InitAchievementData();
        dataShop.InitShopData();
    }
    public void SaveData(List<QuestProfile> questList, List<AchievementProfile> achievementList, PlayerProfile playerProfile, string questEnum, List<ItemProfile> itemList)
    {
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Player", JsonConvert.SerializeObject(playerProfile) },
                { "Quest", JsonConvert.SerializeObject(questList) },
                { "QuestEnum", questEnum },
                { "Time", TimeManager.Hour.ToString() + "/" + TimeManager.Minute.ToString() },
                { "Achievement", JsonConvert.SerializeObject(achievementList) },
                { "Item", JsonConvert.SerializeObject(itemList) },
            }
        };
        PlayFabLeaderboard.instance.SendDataLeaderboard(request);
    }
}


