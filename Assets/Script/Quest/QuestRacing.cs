using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class QuestRacing : QuestObject
{
    [SerializeField] private string message; 
    [SerializeField] private QuestTargetRacing targetRacing;
    [SerializeField] private GameObject targetObject;
    private bool isRunning = false;

    void FixedUpdate()
    {
        if (isRunning)
        {
            QuestRacingUI.instance.CountTimeRunning();
        }

        if (isRunning && targetRacing.isFinish)
        {
            EndRunning();
        }
    }

    public override void HandleLogic()
    {
        StartRunning();
    }
    public void StartRunning()
    {
        targetObject.SetActive(true);
        StartCoroutine(QuestRacingUI.instance.Countdown());
        QuestRacingUI.instance.UpdateTargetText(message);
        isRunning = true;
    }

    private void EndRunning()
    {
        isRunning = false;
        targetObject.SetActive(false);
        QuestManager.instance.CompleteQuest(questProfile.QuestID);
        QuestRacingUI.instance.UpdateTargetText("");
        SubmitScoreToLeaderboard(QuestRacingUI.instance.elapsedTime);
        StartCoroutine(QuestRacingUI.instance.EndRunning());
        
    }
    //private string GetDailyLeaderboardName()
    //{
    //    //TimeZoneInfo timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
    //    //DateTime gmtPlus7Time = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneInfo);

    //    return "RunningQuestTime" + gmtPlus7Time.ToString("yyyyMMdd");
    //}

    private void SubmitScoreToLeaderboard(float time)
    {
        int score = Mathf.FloorToInt(time * 1000);
        PlayFabLeaderboard.instance.SendLeaderboard("RunningDaily", score);
    }

    //private string GetDailyLeaderboardName()
    //{
    //    return "RunningQuestTime_" + DateTime.UtcNow.ToString("yyyyMMdd");
    //}

    //private void SubmitScoreToLeaderboard(float time)
    //{
    //    var request = new UpdatePlayerStatisticsRequest
    //    {
    //        Statistics = new List<StatisticUpdate>
    //        {
    //            new StatisticUpdate { StatisticName = GetDailyLeaderboardName(), Value = Mathf.FloorToInt(time * 100) } // lưu thời gian dưới dạng ms
    //        }
    //    };
    //    PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    //}

    //private void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    //{
    //    Debug.Log("Leaderboard updated successfully.");
    //}

    //private void OnError(PlayFabError error)
    //{
    //    Debug.LogError("Error updating leaderboard: " + error.GenerateErrorReport());
    //}
}
