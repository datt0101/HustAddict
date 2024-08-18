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
using PlayFab.Internal;


public class PlayFabAuth : MonoBehaviour
{
    [SerializeField] private QuestUI questUI;
    public static PlayFabAuth instance; 
    public List<string> allIDList = new List<string>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        PlayFabUI.instance.SetLoginMessage("Thành công!");
        PlayFabUI.instance.TurnOff(PlayFabUI.instance.loginPanel);
        //PlayerManager.instance.gameObject.GetComponent<ThirdPersonController>().enabled = true;
    }

    public void RegisterButton()
    {
        if (PlayFabUI.instance.passwordInputRegister.text.Length < 6)
        {
            PlayFabUI.instance.SetLoginMessage("Mật khẩu phải trên 6 kí tự!");
            return;
        }
        if (PlayFabUI.instance.passwordInputRegister.text == PlayFabUI.instance.rePasswordInputRegister.text)
        {
            var request = new RegisterPlayFabUserRequest
            {
                Email = PlayFabUI.instance.emailInputRegister.text,
                Password = PlayFabUI.instance.passwordInputRegister.text,
                RequireBothUsernameAndEmail = false
            };
            PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSuccess, OnError);
        }
        else
        {
            PlayFabUI.instance.SetLoginMessage("Mật khẩu không trùng khớp !");
        }
    }
    void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        PlayFabUI.instance.SetLoginMessage("Đăng kí thành công!");
        PlayFabUI.instance.TurnOff(PlayFabUI.instance.registerUI);
        PlayFabUI.instance.TurnOn(PlayFabUI.instance.infoUI);
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

    public void InputInforButton()
    {
        Debug.Log("InputInforButton");
        int number;
        bool checkNumber = int.TryParse(PlayFabUI.instance.iDInput.text, out number);
        if (PlayFabUI.instance.nameInput.text.Length < 3)
        {
            PlayFabUI.instance.SetLoginMessage("Tên phải trên 3 kí tự!");
            return;
        }
        if (PlayFabUI.instance.iDInput.text.Length != 8 || !checkNumber)
        {
            PlayFabUI.instance.SetLoginMessage("ID phải đủ 8 chữ số");
            return;
        }
        for (int i = 0; i < allIDList.Count; i++)
        {
            if (PlayFabUI.instance.iDInput.text == allIDList[i])
            {
                PlayFabUI.instance.SetLoginMessage("ID đã tồn tại!");
                return;
            }
        }
        PlayerManager.instance.playerProfile.SetPlayerData(PlayFabUI.instance.iDInput.text, 0, PlayFabUI.instance.nameInput.text, 0, 0, 0, 0, 0);
        allIDList.Add(PlayFabUI.instance.iDInput.text);
        PlayFabClientAPI.UpdateUserTitleDisplayName(new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = PlayFabUI.instance.nameInput.text + "#" + PlayFabUI.instance.iDInput.text
        }, OnDisplayName, OnError
        ); 
        //UpdateDisplayName(PlayFabUI.instance.nameInput.text);
        PlayFabUI.instance.SetLoginMessage("Đang xử lí...");
        ExecuteCloudeScript();
        PlayFabData.instance.SaveData();
        PlayerStatsManager.instance.UpdateLevelUI();
        Login(PlayFabUI.instance.emailInputRegister.text, PlayFabUI.instance.passwordInputRegister.text);
        questUI.ShowAssistantUI(); // show ra Assistant
    }
    void OnDisplayName(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name Displayed!");
    }
    public void LoginButton()
    {
        Login(PlayFabUI.instance.emailInputLogin.text, PlayFabUI.instance.passwordInputLogin.text);
    }
    public void Login(string email, string password)
    {
        Debug.Log("LoginButton");
        var request = new LoginWithEmailAddressRequest
        {
            Email = email,
            Password = password
        };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }
    public void ResetPasswordButton()
    {
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = PlayFabUI.instance.emailInputLogin.text,
            TitleId = "C3EE0",
        };
        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnPasswordReset, OnError);
    }
    void OnPasswordReset(SendAccountRecoveryEmailResult result)
    {
        PlayFabUI.instance.SetLoginMessage("Đã gửi email khôi phục mật khẩu!");
    }
    void OnLoginSuccess(LoginResult result)
    {
        //playerProfile = PlayerManager.instance.playerProfile;
        PlayFabData.instance.GetData();
        PlayFabUI.instance.SetLoginMessage("Đăng nhập thành công");
        
    }

    void OnError(PlayFabError error)
    {
        PlayFabUI.instance.SetLoginMessage(error.ErrorMessage);
        Debug.Log(error.GenerateErrorReport());
    }
}