using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;


public class PlayFabUI : MonoBehaviour
{
  
    static public PlayFabUI instance;
    //Panel
    public GameObject loginPanel;
    public GameObject registerUI;
    public GameObject loginUI;
    public GameObject infoUI;
    public Text loginMessage;

    //Leaderboard
    public Transform leaderboardID;
    public Transform leaderboardName;
    public Transform leaderboardValue;

    //Auth
    public InputField emailInputLogin;
    public InputField passwordInputLogin;

    public InputField emailInputRegister;
    public InputField passwordInputRegister;
    public InputField rePasswordInputRegister;
    public InputField nameInput;
    public InputField iDInput;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void TurnOn (GameObject gameObject)
    {
        Debug.Log("Turn ON" + gameObject);
        gameObject.SetActive(true);
    }
    public void TurnOff(GameObject gameObject)
    {
        Debug.Log("Turn OFF" + gameObject);
        gameObject.SetActive(false);
    }
    public void SetLoginMessage(string message)
    {
        loginMessage.text = message;
    }

    public void UpdateLeaderboardUI(int position,string PlayFabId, string DisplayName, int StatValue)
    {
        string[] displayName = DisplayName.Split(char.Parse("#"));
        leaderboardID.GetChild(position).GetComponent<Text>().text = displayName[1] != null ? displayName[1] : "Unknown";
        //leaderboardID.GetChild(item.Position).GetComponent<Text>().text = PlayFabAuth.instance.allIDList.Count > item.Position ? PlayFabAuth.instance.allIDList[item.Position] : "Unknown ID";
        leaderboardName.GetChild(position).GetComponent<Text>().text = displayName[0] != null ? displayName[0] : "Unknown";
        leaderboardValue.GetChild(position).GetComponent<Text>().text = StatValue.ToString();
    }
    public void UpdateRaceLeaderboardUI(int position, string PlayFabId, string DisplayName, int StatValue)
    {
        string[] displayName = DisplayName.Split(char.Parse("#"));
        leaderboardID.GetChild(position).GetComponent<Text>().text = displayName[1] != null ? displayName[1] : "Unknown";
        //leaderboardID.GetChild(item.Position).GetComponent<Text>().text = PlayFabAuth.instance.allIDList.Count > item.Position ? PlayFabAuth.instance.allIDList[item.Position] : "Unknown ID";
        leaderboardName.GetChild(position).GetComponent<Text>().text = displayName[0] != null ? displayName[0] : "Unknown";

        int sec = StatValue / 1000;
        int mili = StatValue - sec * 1000;
        leaderboardValue.GetChild(position).GetComponent<Text>().text = sec.ToString() + ":" + mili.ToString() + "s";

    }

    public void EmptyUI()
    {
        for (int position = 0; position < leaderboardID.childCount; ++position)
        {
            leaderboardID.GetChild(position).GetComponent<Text>().text = "";
            //leaderboardID.GetChild(item.Position).GetComponent<Text>().text = PlayFabAuth.instance.allIDList.Count > item.Position ? PlayFabAuth.instance.allIDList[item.Position] : "Unknown ID";
            leaderboardName.GetChild(position).GetComponent<Text>().text = "";
            leaderboardValue.GetChild(position).GetComponent<Text>().text = "";
        }
    }
    //public void UpdateAuthUI(string email, string password)
    //{
    //    email = emailInput.text;
    //    password = passwordInput.text;
    //}
}

