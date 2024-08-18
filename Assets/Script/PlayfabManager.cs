using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;

using System;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;
using Unity.VisualScripting;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;

public class PlayfabManager : MonoBehaviour
{
    public static PlayfabManager instance;
    public PlayerProfile playerProfile;
    public Text loginMessage;
    public InputField emailInput;
    public InputField passwordInput;
    public InputField nameInput;
    public InputField iDInput;
    public GameObject loginPanel;
    public GameObject registerUI;
    public GameObject loginUI;
    public Transform leaderboardID;
    public Transform leaderboardName;
    public Transform leaderboardValue;

    public List<string> allIDList = new List<string>();
    public List<QuestProfile> questList;
    public List<AchievementProfile> achievementList;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataReceived, OnError);
    }
    void OnTitleDataReceived(GetTitleDataResult result)
    {
        if (result.Data != null || result.Data.ContainsKey("IDList"))
        {
            Debug.Log("Data completed");
            allIDList = JsonConvert.DeserializeObject<List<string>>(result.Data["IDList"]);
        }
        else
        {
            Debug.Log("No ID in list");
        }

    }
    public void ExecuteCloudeScript()
    {
        var request = new ExecuteCloudScriptRequest
        {
            FunctionName = "titleDataSet",
            FunctionParameter = new
            {
                id = JsonConvert.SerializeObject(allIDList)
            }
        };
        PlayFabClientAPI.ExecuteCloudScript(request, OnExecuteSuccess, OnError);
    }
    void OnExecuteSuccess(ExecuteCloudScriptResult result)
    {
        loginMessage.text = "Thành công!";
        loginPanel.SetActive(false);
        //PlayerManager.instance.gameObject.GetComponent<ThirdPersonController>().enabled = true;
    }
    public void RegisterButton()
    {
        if (passwordInput.text.Length < 6)
        {
            loginMessage.text = "Mật khẩu phải trên 6 kí tự!";
            return;
        }
        var request = new RegisterPlayFabUserRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
            RequireBothUsernameAndEmail = false
        };
        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        
        loginMessage.text = "Đăng kí thành công!";
        
            
    }
    public void InputInforButton()
    {
        Debug.Log("InputInforButton");
        int number;
        bool checkNumber = int.TryParse(iDInput.text, out number);
        if (nameInput.text.Length < 3)
        {
            loginMessage.text = "Tên phải trên 3 kí tự!";
            return;
        }
        if (iDInput.text.Length != 8 || !checkNumber)
        {
            loginMessage.text = "ID phải đủ 8 chữ số";
            return;
        }
        for (int i = 0; i < allIDList.Count; i++)
        {
            if (iDInput.text == allIDList[i])
            {
                loginMessage.text = "ID đã tồn tại!";
                return;
            }
        }
        playerProfile.SetPlayerData(iDInput.text, 0, nameInput.text, 0, 0, 0, 0,0);
        allIDList.Add(iDInput.text);
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text
        }, OnDisplayName, OnError
        );
        loginMessage.text = "Đang xử lí...";
        ExecuteCloudeScript();
        SaveData();
        PlayerStatsManager.instance.UpdateLevelUI();
        LoginButton();
    }
    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name Displayed!");
    }
    public void LoginButton()
    {
        Debug.Log("LoginButton");
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailInput.text,
            Password = passwordInput.text,
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailInput.text,
            TitleId = "C3EE0",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        loginMessage.text = "Đã gửi email khôi phục mật khẩu!";
    }
    void OnLoginSuccess(LoginResult result)
    {
        playerProfile = PlayerManager.instance.playerProfile;
        GetData();
        loginMessage.text = "Đăng nhập thành công";

    }
    void OnError(PlayFabError error)
    {
        loginMessage.text = error.ErrorMessage;
        Debug.Log(error.GenerateErrorReport());
    }
    public void SendLeaderboard(string leaderboardName, int value)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = leaderboardName,
                    Value = value
                }
             }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }
    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult result)
    {
        Debug.Log("Sent to Leaderboard succesfully");
    }
    public void GetLeaderboard(string leaderboardName)
    {
        var request = new GetLeaderboardRequest
        {
            StatisticName = leaderboardName,
            StartPosition = 0,
            MaxResultsCount = 10
        };
        PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
    }
    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            leaderboardID.GetChild(item.Position).GetComponent<Text>().text = item.PlayFabId.ToString();
            leaderboardName.GetChild(item.Position).GetComponent<Text>().text = item.DisplayName.ToString();
            leaderboardValue.GetChild(item.Position).GetComponent<Text>().text = item.StatValue.ToString();
            
            Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }
    public void GetData()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataReceived, OnError);
    }
    void OnDataReceived(GetUserDataResult result)
    {
        if (result.Data != null && result.Data.ContainsKey("Player"))
        {
            Debug.Log("Data Received");
            GetTitleData();
            loginPanel.SetActive(false);
            //FindObjectOfType<ThirdPersonController>().gameObject.GetComponent<ThirdPersonController>().enabled = true;
            // xu li data cua thoi gian
            string[] timeData = result.Data["Time"].Value.Split(char.Parse("/"));
            TimeManager.instance.SetTime(int.Parse(timeData[0]), int.Parse(timeData[1]));
            OnQuestDataReceived(result); // xu li data cua quest
            OnPlayerDataReceived(result); // xu li data cua player
            OnAchievementDataReceived(result); // xu li data cua achievement
        }
        else
        {
            GetTitleData();
            loginUI.SetActive(false);
            registerUI.SetActive(true);
            loginMessage.text = "Chào mừng người chơi mới!";
            QuestManager.instance.InitQuestList();
            AchievementManager.instance.InitAchievementList();
        }
    }
    void OnQuestDataReceived(GetUserDataResult result)
    {
        // xu li data quest List
        QuestProfile[] allQuestProfiles = Resources.LoadAll<QuestProfile>("Quests"); ;
        var questListData = JsonConvert.DeserializeObject<List<QuestProfile>>(result.Data["Quest"].Value);
        questList = MapQuestProfiles(allQuestProfiles, questListData);
        // xu li data cua QuestProgress
        string questEnumData = result.Data["QuestEnum"].Value;
        string[] questEnumList = questEnumData.Split(char.Parse("/"));
        QuestProgress questProgress;
        QuestType questType;
        QuestReward questReward;
        for (int i = 0; i < questList.Count; i++)
        {
            string[] splitElement = questEnumList[i].Split(char.Parse(","));
            Enum.TryParse<QuestProgress>(splitElement[0], out questProgress);
            Enum.TryParse<QuestType>(splitElement[1], out questType);
            Enum.TryParse<QuestReward>(splitElement[2], out questReward);
            questList[i].QuestProgress = questProgress;
            questList[i].QuestType = questType;
            questList[i].QuestReward = questReward;
        }
        QuestManager.instance.UpdateQuestAccepted();
        PlayerStatsManager.instance.UpdateLevelUI();
    }
    void OnAchievementDataReceived(GetUserDataResult result)
    {
        // xu li data quest List
        AchievementProfile[] allAchievementProfiles = Resources.LoadAll<AchievementProfile>("Achievements"); ;
        var achievementListData = JsonConvert.DeserializeObject<List<AchievementProfile>>(result.Data["Achievement"].Value);
        Debug.Log(JsonConvert.DeserializeObject<List<AchievementProfile>>(result.Data["Achievement"].Value));
        achievementList = MapAchievementProfiles(allAchievementProfiles, achievementListData);

        //for (int i = 0;i<achievementList.Count;i++)
        //{
        //    Debug.Log("After Map" + achievementList[i].AchievementStatus);
        //}
        AchievementManager.instance.UploadUnlockAchievement();
    }
    void OnPlayerDataReceived(GetUserDataResult result)
    {
        PlayerProfile PlayerProfileResources = Resources.Load<PlayerProfile>("PlayerProfile");
        var playerProfileData = JsonConvert.DeserializeObject<PlayerProfile>(result.Data["Player"].Value);
        //playerProfile.SetPlayerData(result.Data["ID"].Value, int.Parse(result.Data["Level"].Value), result.Data["Name"].Value, 
        //                           int.Parse(result.Data["Strength"].Value), int.Parse(result.Data["Intelligence"].Value),
        //                           int.Parse(result.Data["Social"].Value), int.Parse(result.Data["Knowledge"].Value));
        //playerProfile.SetPlayerData(playerProfileData.PlayerID, playerProfileData.PlayerLevel, playerProfileData.PlayerName,
        //                            playerProfileData.PlayerStrength, playerProfileData.PlayerIntelligence,
        //                            playerProfileData.PlayerSocial,playerProfileData.PlayerKnowledge);
        playerProfile = LoadProfile(PlayerProfileResources, playerProfileData);
        PlayerStatsManager.instance.UpdateLevelUI();
    }
    
    private List<QuestProfile> MapQuestProfiles(QuestProfile[] allQuestProfiles, List<QuestProfile> questDataList)
    {
        List<QuestProfile> questProfiles = new List<QuestProfile>();

        foreach (var questData in questDataList)
        {
            // Tìm `QuestProfile` phù hợp từ tài sản
            QuestProfile questProfile = allQuestProfiles.FirstOrDefault(q => q.QuestID == questData.QuestID);
            if (questProfile != null)
            {
                questProfile.QuestTitle = questData.QuestTitle;
                questProfile.QuestDescription = questData.QuestDescription;
                questProfile.QuestLevel = questData.QuestLevel; // quest theo level cua sinh vien ( 1,2,3,4)
                questProfile.QuestID = questData.QuestID;
                questProfile.QuestSummary = questData.QuestSummary;// tom tat nv
                questProfile.QuestPoint = questData.QuestPoint; // diem thuong
                questProfile.QuestAnswer = questData.QuestAnswer;
                // Khởi tạo các thuộc tính khác nếu cần
                questProfiles.Add(questProfile);
            }
            else
            {
                Debug.LogWarning($"QuestProfile with ID {questData.QuestID} not found.");
            }
        }
        return questProfiles;
    }
    private List<AchievementProfile> MapAchievementProfiles(AchievementProfile[] allAchievementProfiles, List<AchievementProfile> achievementDataList)
    {
        List<AchievementProfile> achievementProfiles = new List<AchievementProfile>();

        foreach (var achievementData in achievementDataList)
        {
 
            Debug.Log("Map:" + achievementData.AchievementStatus);
            // Tìm `QuestProfile` phù hợp từ tài sản
            AchievementProfile achievementProfile = allAchievementProfiles.FirstOrDefault(q => q.AchievementID == achievementData.AchievementID);
            if (achievementProfile != null)
            {
                achievementProfile.AchievementID = achievementData.AchievementID;
                achievementProfile.AchievementName = achievementData.AchievementName;
                achievementProfile.AchievementDescription = achievementData.AchievementDescription;
                achievementProfile.AchievementReward = achievementData.AchievementReward;
                achievementProfile.AchievementStatus = achievementData.AchievementStatus;
                // Khởi tạo các thuộc tính khác nếu cần
                achievementProfiles.Add(achievementProfile);
            }
            else
            {
                Debug.LogWarning($"QuestProfile with ID {achievementData.AchievementID} not found.");
            }
        }
        return achievementProfiles;
    }
    private  PlayerProfile LoadProfile(PlayerProfile profileRoot, PlayerProfile profileData)
    {
        PlayerProfile playerProfle = new PlayerProfile();
        
        profileRoot.PlayerID = profileData.PlayerID;
        profileRoot.PlayerLevel = profileData.PlayerLevel;
        profileRoot.PlayerName = profileData.PlayerName;
        profileRoot.PlayerIntelligence = profileData.PlayerIntelligence;
        profileRoot.PlayerSocial = profileData.PlayerSocial;
        profileRoot.PlayerKnowledge = profileData.PlayerKnowledge;
        profileRoot.PlayerStrength = profileData.PlayerStrength;

        playerProfle = profileRoot;
        return playerProfile;
    }
    public void SaveData()
    {
        Debug.Log("SaveData");
        questList = QuestManager.instance.questList;
        playerProfile = PlayerManager.instance.playerProfile;
        achievementList = AchievementManager.instance.achievementProfileList;
        string questEnum = "";
        for (int i = 0;i< questList.Count; i++)
        {
            questEnum += questList[i].QuestProgress + "," + questList[i].QuestType + "," + questList[i].QuestReward + "/";
        }
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                { "Player", JsonConvert.SerializeObject(playerProfile) },
                { "Quest", JsonConvert.SerializeObject(questList) },
                { "QuestEnum", questEnum },
                { "Time", TimeManager.Hour.ToString() + "/" + TimeManager.Minute.ToString() },
                { "Achievement", JsonConvert.SerializeObject(achievementList) },
            }
            
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }
    void OnDataSend(UpdateUserDataResult result)
    {
        SendLeaderboard("Level", playerProfile.PlayerLevel);
        SendLeaderboard("Strength", playerProfile.PlayerStrength);
        SendLeaderboard("Intelligence", playerProfile.PlayerIntelligence);
        SendLeaderboard("Social", playerProfile.PlayerSocial);
        SendLeaderboard("Knowledge", playerProfile.PlayerKnowledge);
    }

    private void OnApplicationQuit()
    {
        Debug.Log("OnApplicationQuit");
        SaveData();
    }
}


