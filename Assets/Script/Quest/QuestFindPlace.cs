using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class QuestFindPlace : QuestObject
{
    public override void HandleLogic()
    {
        Debug.Log("FindPlace");
        QuestManager.instance.CompleteQuest(questProfile.QuestID);
    }
}

