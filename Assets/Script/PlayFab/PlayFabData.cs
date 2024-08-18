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
using System.Runtime.CompilerServices;

public class PlayFabData: MonoBehaviour
{
    public static PlayFabData instance;
    [SerializeField] private PlayerProfile playerProfile;
    [SerializeField] private List<QuestProfile> questList;
    [SerializeField] private List<AchievementProfile> achievementList;
    [SerializeField] private List<ItemProfile> itemList;
    [SerializeField] private PlayFabUI playFabUI;
    [SerializeField] private DataHandler dataHandler;
    public PlayerProfile PlayerProfile { get => playerProfile; }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    
    void OnError(PlayFabError error)
    {
        Debug.Log(error.GenerateErrorReport());
    }
    public void GetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }

    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Player"))
        {
            Debug.Log("Data Received");
            //PlayFabAuth.instance.GetTitleData();
            playFabUI.TurnOff(playFabUI.loginPanel);
            dataHandler.ReceiveData(result, questList, achievementList, playerProfile,itemList);
        }
        else
        {
            //PlayFabAuth.instance.GetTitleData();
            Debug.Log("Else");
            playFabUI.TurnOff(playFabUI.loginUI);
            playFabUI.TurnOff(playFabUI.registerUI);
            playFabUI.TurnOn(playFabUI.infoUI);
            playFabUI.SetLoginMessage("Chào mừng người chơi mới!");
            dataHandler.InitData();
        }
    }
    public void SaveData()
    {
        Debug.Log("SaveData");
        questList = QuestManager.instance.questList;
        playerProfile = PlayerManager.instance.playerProfile;
        achievementList = AchievementManager.instance.achievementProfileList;
        itemList = ShopManager.instance.itemList;
        string questEnum = "";
        for (int i = 0; i < questList.Count; i++)
        {
            questEnum += questList[i].QuestProgress + "," + questList[i].QuestType + "," + questList[i].QuestReward + "/";
        }
        dataHandler.SaveData(questList, achievementList, playerProfile, questEnum,itemList);
    }

    private void OnApplicationQuit()
    {
        SaveData();
    }
}


