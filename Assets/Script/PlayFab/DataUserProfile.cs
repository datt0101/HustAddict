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
using UnityEngine.EventSystems;
using UnityEngine.Windows;


public class DataUserProfile : MonoBehaviour
{
    public static DataUserProfile instance;
   
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void OnPlayerDataReceived(GetUserDataResult result, PlayerProfile playerProfile)
    {
        //profile
        PlayerProfile PlayerProfileResources = Resources.Load<PlayerProfile>("PlayerProfile");
        var playerProfileData = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player"].Value);
        playerProfile = LoadProfile(PlayerProfileResources, playerProfileData);
        PlayerStatsManager.instance.UpdateLevelUI();

        ////Position
        //string playerPositionData = result.Data["PlayerPosition"].Value.Trim('(', ')');
        //string[] valuesPosition = playerPositionData.Split(',');  // Tách các giá trị x, y, z
        //float x = float.Parse(valuesPosition[0]);
        //float y = float.Parse(valuesPosition[1]);
        //float z = float.Parse(valuesPosition[2]);
        //playerPosition.position = new Vector3(x, y, z);
    }

    public PlayerProfile LoadProfile(PlayerProfile profileRoot, PlayerProfile profileData)
    {
        PlayerProfile playerProfle = new PlayerProfile();

        profileRoot.PlayerID = profileData.PlayerID;
        profileRoot.PlayerLevel = profileData.PlayerLevel;
        profileRoot.PlayerName = profileData.PlayerName;
        profileRoot.PlayerIntelligence = profileData.PlayerIntelligence;
        profileRoot.PlayerSocial = profileData.PlayerSocial;
        profileRoot.PlayerKnowledge = profileData.PlayerKnowledge;
        profileRoot.PlayerStrength = profileData.PlayerStrength;
        profileRoot.PlayerCredit = profileData.PlayerCredit;
        playerProfle = profileRoot;

        return playerProfle;
    }

}


