using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject blackOut;
    public GameObject blackPanel;
    public GameObject leaderboardPanel;
    public GameObject shopPanel;

    //Player Profile 
    public Text playerName;
    public Text playerID;
    public Text playerLevel;
    public Text playerStrength;
    public Text playerIntelligence;
    public Text playerSocial;
    public Text playerKnowledge;

    //Exp Bar and level in Main screen
    public TMP_Text playerLevelText;
    public Image ExpBar;
    public TMP_Text playerNameText;

    //Player Profile on Setting Canvas
    public Text playerNameSetting;
    public Text playerIDSetting;
    public Text playerLevelSetting;
    private void Awake()
    {
        instance = this;
    }
    public void BlackIn()
    {
        blackOut.SetActive(true);
        blackOut.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
        blackOut.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 1f)
            .OnComplete(() =>
            {
                blackOut.SetActive(false);
            });
    }
    public void BlackOut()
    {
        blackOut.SetActive(true);
        blackOut.GetComponent<Image>().color = new Color(0, 0, 0, 0f);
        blackOut.GetComponent<Image>().DOColor(new Color(0, 0, 0, 1), 1f);
    }
    public void TurnOn(GameObject panel)
    {
        blackPanel.SetActive(true);
        panel.SetActive(true);
        panel.transform.localScale = Vector3.zero;
        panel.transform.DOScale(1f, .5f);
        if (panel.name == "PlayerProfile")
            UpdateProfile();
        else if (panel.name == "SettingCanvas")
            UpdateProfileOnSettingCanvas();

    }
    public void TurnOff(GameObject panel)
    {
        panel.transform.DOScale(0f, .5f)
            .OnComplete(() =>
            {
                blackPanel.SetActive(false);
                panel.SetActive(false);
            });
    }
    public void LeaderboardOn(GameObject firstPanel)
    {
        firstPanel.transform.DOScale(0f, .5f)
            .OnComplete(() =>
            {
                firstPanel.SetActive(false);
                leaderboardPanel.SetActive(true);
                leaderboardPanel.transform.localScale = Vector3.zero;
                leaderboardPanel.transform.DOScale(1f,.5f); 
            });
    }
    public void ShopOn(GameObject firstPanel)
    {
        firstPanel.transform.DOScale(0f, .5f)
            .OnComplete(() =>
            {
                firstPanel.SetActive(false);
                shopPanel.SetActive(true);
                shopPanel.transform.localScale = Vector3.zero;
                shopPanel.transform.DOScale(1f, .5f);
            });
    }
    public void UpdateProfileOnSettingCanvas()
    {
        playerNameSetting.text ="Tên: " + PlayerManager.instance.playerProfile.PlayerName;
        playerIDSetting.text = "ID: " + PlayerManager.instance.playerProfile.PlayerID;
        playerLevelSetting.text = "Cấp: " + PlayerManager.instance.playerProfile.PlayerLevel.ToString();
    }
    public void UpdateProfile()
    {
        playerName.text = "Tên: " + PlayerManager.instance.playerProfile.PlayerName;
        playerID.text = "ID: " + PlayerManager.instance.playerProfile.PlayerID;
        playerLevel.text = "Cấp: " + PlayerManager.instance.playerProfile.PlayerLevel.ToString();
        playerStrength.text = "Strength: " + PlayerManager.instance.playerProfile.PlayerStrength.ToString();
        playerIntelligence.text = "Intelligence: " + PlayerManager.instance.playerProfile.PlayerIntelligence.ToString();
        playerSocial.text = "Social: " + PlayerManager.instance.playerProfile.PlayerSocial.ToString();
        playerKnowledge.text = "Knowledge: " + PlayerManager.instance.playerProfile.PlayerKnowledge.ToString();

    }
    public void UpdateLevelDisplay(int ValueOfOneLevel)
    {
        playerLevelText.text = "Cấp " + PlayerManager.instance.playerProfile.PlayerLevel.ToString(); 
        ExpBar.fillAmount  = (float) (PlayerManager.instance.playerProfile.PlayerSocial + PlayerManager.instance.playerProfile.PlayerIntelligence
            + PlayerManager.instance.playerProfile.PlayerKnowledge + PlayerManager.instance.playerProfile.PlayerStrength 
            - ValueOfOneLevel* PlayerManager.instance.playerProfile.PlayerLevel) 
            / ValueOfOneLevel;

        playerNameText.text = PlayerManager.instance.playerProfile.PlayerName;
    }
}
