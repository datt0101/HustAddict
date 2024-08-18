using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;
using System.Linq;


public class PlayFabLeaderboard : MonoBehaviour
{
    public static PlayFabLeaderboard instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void SendLeaderboard(string leaderboardName, int value)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = leaderboardName,
                    Value = value
                }
             }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sent to Leaderboard succesfully");
    }
    public void GetLeaderboard(string leaderboardName)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        PlayFabUI.instance.EmptyUI();
        foreach (var item in result.Leaderboard)
        {
            if (item == null)
            {

                continue;
            }
            PlayFabUI.instance.UpdateLeaderboardUI(item.Position, item.PlayFabId, item.DisplayName, item.StatValue);
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    public void SendDataLeaderboard(UpdateUserDataRequest request)
    {
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void OnDataSend(UpdateUserDataResult result)
    {
        SendLeaderboard("Level", PlayFabData.instance.PlayerProfile.PlayerLevel);
        SendLeaderboard("Strength", PlayFabData.instance.PlayerProfile.PlayerStrength);
        SendLeaderboard("Intelligence", PlayFabData.instance.PlayerProfile.PlayerIntelligence);
        SendLeaderboard("Social", PlayFabData.instance.PlayerProfile.PlayerSocial);
        SendLeaderboard("Knowledge", PlayFabData.instance.PlayerProfile.PlayerKnowledge);
    }
    void OnError(PlayFabError error)
    {

        Debug.Log(error.GenerateErrorReport());
    }

    public void GetRaceLeaderboard(string leaderboardName)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = 5
        };
        PlayFabClientAPI.GetLeaderboard(request, OnRaceLeaderboardGet, OnError);
    }
    void OnRaceLeaderboardGet(GetLeaderboardResult result)
    {
        PlayFabUI.instance.EmptyUI();
        List<PlayerLeaderboardEntry> leaderboard = result.Leaderboard;
        int l = leaderboard.Count;
        for (int i = l-1; i >= 0; i--)
        {
            {
                if (leaderboard[i] == null)
                {
                    Debug.Log(i);
                    continue;
                }
                PlayFabUI.instance.UpdateRaceLeaderboardUI(l - 1 - i, leaderboard[i].PlayFabId, leaderboard[i].DisplayName, leaderboard[i].StatValue);
            }
        }
    }
}
