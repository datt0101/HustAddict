using PlayFab.ClientModels;
using PlayFab;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class QuestTargetRacing : MonoBehaviour
{
    public bool isFinish = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isFinish = true;
        }
    }

    private void OnDisable()
    {
        isFinish = false;
    }
}
