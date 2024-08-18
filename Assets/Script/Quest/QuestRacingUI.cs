using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class QuestRacingUI : MonoBehaviour
{
    public static QuestRacingUI instance;
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text countdownText;
    [SerializeField] private GameObject timerPanel;
    [SerializeField] private TMP_Text targetText;
    [SerializeField] private GameObject CountdownPanel;
    [SerializeField] private GameObject CountTimePanel;
    private float startTime;
    public float elapsedTime = 0f;
  
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void UpdateTargetText(string targetText)
    {
        this.targetText.text = targetText;
    }
    public void CountTimeRunning()
    {

        elapsedTime = Time.time - startTime;
        timerText.text = elapsedTime.ToString("F2");
    }

    public IEnumerator Countdown()
    {
        timerPanel.SetActive(true);
     
        CountdownPanel.SetActive(true);
        for (int i = 3; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1.0f);
        }
        countdownText.text = "";
        startTime = Time.time;
        CountdownPanel.SetActive(false);
        CountTimePanel.SetActive(true);
    }
 
    public IEnumerator EndRunning()
    {
        startTime = 0f;
        elapsedTime = 0f; 
        timerPanel.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        timerText.text = "";
        CountTimePanel?.SetActive(false);

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
