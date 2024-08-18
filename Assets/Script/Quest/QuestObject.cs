using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class QuestObject : MonoBehaviour
{
    public QuestProfile questProfile;
    public abstract void HandleLogic();
    protected virtual void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "Player" || other.tag == "PlayerInteract") && questProfile.QuestProgress == QuestProgress.Accepted)
        {
            Debug.Log("Trigger");
            HandleLogic();
        }
    }

   

}

