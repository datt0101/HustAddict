using DG.Tweening;
using System;
using System.Drawing;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager instance;
    [SerializeField] private GameObject addStatsText;
    [SerializeField] private GameObject levelUpText;
    [SerializeField] private int ValueOfOneLevel = 50;

    public int ValueOfOneLevel1 { get => ValueOfOneLevel; set => ValueOfOneLevel = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        UpdateLevelUI();
    }

    public void AddStats(QuestProfile questProfile)
    {
        if (questProfile == null)
        {
            Debug.Log("questProfile is Null !!!");
        }
        switch (questProfile.QuestReward)
        {
            case QuestReward.Intelligence:
                PlayerManager.instance.playerProfile.AddIntelligence(questProfile.QuestPoint);
                AddCredit(questProfile.QuestPoint);
                ActiveEffect(questProfile.QuestPoint, "Trí tuệ");
                AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerIntelligence, "Trí tuệ");
                break;

            case QuestReward.Social:
                PlayerManager.instance.playerProfile.AddSocial(questProfile.QuestPoint);
                AddCredit(questProfile.QuestPoint);
                ActiveEffect(questProfile.QuestPoint, "Xã hội");
                AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerSocial, "Xã hội");
                break;

            case QuestReward.Strength:
                PlayerManager.instance.playerProfile.AddStrength(questProfile.QuestPoint);
                AddCredit(questProfile.QuestPoint);
                ActiveEffect(questProfile.QuestPoint, "Sức mạnh");
                AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerStrength, "Sức mạnh");
                break;

            case QuestReward.Knowledge:
                PlayerManager.instance.playerProfile.AddKnowledge(questProfile.QuestPoint);
                AddCredit(questProfile.QuestPoint);
                ActiveEffect(questProfile.QuestPoint, "Hiểu biết");
                AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerKnowledge, "Hiểu biết");
                break;

            default:
                break;  
        }
        UpdateLevelUI();
        CheckLevelUpdate();
        PlayFabData.instance.SaveData();
    }
    public void AddStats(int point, string questReward)
    {
        QuestReward reward;
        if (Enum.TryParse(questReward, out reward))
        {
            switch (reward)
            {
                case QuestReward.Intelligence:
                    PlayerManager.instance.playerProfile.AddIntelligence(point);
                    ActiveEffect(point, "Trí tuệ");
                    AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerIntelligence, "Trí tuệ");
                    break;

                case QuestReward.Social:
                    PlayerManager.instance.playerProfile.AddSocial(point);
                    ActiveEffect(point, "Xã hội");
                    AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerSocial, "Xã hội");
                    break;

                case QuestReward.Strength:
                    PlayerManager.instance.playerProfile.AddStrength(point);
                    ActiveEffect(point, "Sức mạnh");
                    AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerStrength, "Sức mạnh");
                    break;

                case QuestReward.Knowledge:
                    PlayerManager.instance.playerProfile.AddKnowledge(point);
                    ActiveEffect(point, "Hiểu biết");
                    AchievementManager.instance.CheckUnlockStatsAchievement(PlayerManager.instance.playerProfile.PlayerKnowledge, "Hiểu biết");
                    break;

                default:
                    break;
            }
        }
        UpdateLevelUI();
        CheckLevelUpdate();
        PlayFabData.instance.SaveData();
    }
    public int CalculatePlayerLevel()
    {
        return (PlayerManager.instance.playerProfile.PlayerSocial + PlayerManager.instance.playerProfile.PlayerIntelligence
            + PlayerManager.instance.playerProfile.PlayerKnowledge + PlayerManager.instance.playerProfile.PlayerStrength) / ValueOfOneLevel;
    }
    public void AddCredit(int point)
    {
        PlayerManager.instance.playerProfile.AddCredit(point);
    }
    public void CheckLevelUpdate()
    {
        if (PlayerManager.instance.playerProfile.PlayerLevel != CalculatePlayerLevel())
        {
            PlayerManager.instance.playerProfile.PlayerLevel = CalculatePlayerLevel();
            UpdateLevelUI();
            levelUpText.SetActive(true);
            levelUpText.GetComponentInChildren<TMP_Text>().text = "TĂNG CẤP!!";
            levelUpText.transform.localScale = Vector3.zero;
            levelUpText.transform.DOScale(1.5f, 1f)
                .OnComplete(() =>
                {
                    DOVirtual.DelayedCall(1f, () =>
                    {
                        levelUpText.transform.DOScale(0f, .5f)
                        .OnComplete(() =>
                        {
                            levelUpText.SetActive(false);
                        });
                    });
                });

        }
    }
    public void UpdateLevelUI()
    {
        UIManager.instance.UpdateLevelDisplay(ValueOfOneLevel);
    }
    public void ActiveEffect(int point, string content)
    {
        Debug.Log("Active effect");
        addStatsText.SetActive(true);
        addStatsText.GetComponentInChildren<TMP_Text>().text = "+" + point.ToString() + " " + content;
        addStatsText.transform.localScale = Vector3.zero;
        addStatsText.transform.DOScale(1, .5f)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(.5f, () =>
                {
                    addStatsText.transform.DOScale(0f, .5f)
                    .OnComplete(() =>
                    {
                        addStatsText.SetActive(false);
                    });
                });
            });
    }
}



