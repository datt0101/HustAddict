using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class QuestStudy : QuestObject
{
    [SerializeField] private StudyMinigameManager studyMinigameManager;
    [SerializeField] private LessonProfile[] lessonProfiles;
    public override void HandleLogic()
    {
        ActiveMinigame();
        QuestManager.instance.CompleteQuest(questProfile.QuestID);
    }
    public void ActiveMinigame()
     {
        if (Random.value < 1f)
        {
            int min = 0;
            int max = lessonProfiles.Length;
            int randomNum = Random.Range(min, max);
            studyMinigameManager.ActiveMinigame(lessonProfiles[randomNum]);
        }
     }

    
}

