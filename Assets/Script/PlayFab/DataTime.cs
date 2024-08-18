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


public class DataTime : MonoBehaviour
{
    public static DataTime instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void InitTimeData()
    {
        TimeManager.instance.InitTime();
    }
    public void OnTimeDataReceived(GetUserDataResult result)
    {
        string[] timeData = result.Data["Time"].Value.Split(char.Parse("/"));
        TimeManager.instance.SetTime(int.Parse(timeData[0]), int.Parse(timeData[1]));
        Debug.Log(TimeManager.Hour.ToString() + "/" + TimeManager.Minute.ToString());
    }

}


