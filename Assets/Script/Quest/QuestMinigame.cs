using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuestMinigame : QuestObject
{
    public GameObject mainPanel;
    public GameObject minigameName;
    public GameObject mainCamera;
    public Ball ball;

    public override void HandleLogic()
    {
        Debug.Log("Collision QuestObject");
        mainCamera.SetActive(false);
        TimeManager.instance.gameObject.SetActive(false);
        minigameName.SetActive(true);
        mainPanel.SetActive(false);
        ball.QuestID = questProfile.QuestID;
    }

   
}

