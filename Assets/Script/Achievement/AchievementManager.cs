
using System.Collections.Generic;
using UnityEngine;


public class AchievementManager : MonoBehaviour
{
    public static AchievementManager instance;
    [SerializeField] private List<AchievementSlot> achievementSlotList;
    public List<AchievementProfile> achievementProfileList;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);

        InitAchievementList();
    }
    public void InitAchievementList()
    {
        int l = achievementProfileList.Count;
        for (int i = 0;i<l;++i)
        {
            achievementProfileList[i].AchievementID = i;
            achievementProfileList[i].AchievementStatus = false;
        }
    }
    public void UploadUnlockAchievement()
    {
        foreach (var profile in achievementSlotList)
        {
            if (profile.AchievementProfile.AchievementStatus)
            {
                Debug.Log("check status");
                profile.UnlockAchievementSlot();
            }
        }
    }
    public void UnlockAchievement(string title)
    {
        foreach(var profile in achievementSlotList)
        {  
            if (title == profile.AchievementProfile.AchievementName && !profile.AchievementProfile.AchievementStatus)
            {
                profile.AchievementProfile.AchievementStatus = true;
                profile.UnlockAchievementSlot();
                AchievementUI.instance.ShowAchievementEffect(profile.AchievementProfile.AchievementName);
                PlayerStatsManager.instance.AddCredit(profile.AchievementProfile.AchievementReward);
                PlayFabData.instance.SaveData();
            }
        }
    }

    public void CheckUnlockStatsAchievement(int value, string nameAchievement)
    {
        switch(value)
        {
            case < 100:
                break;
            case < 200:
                UnlockAchievement(nameAchievement + " 1");
                break;
            case < 400:
                UnlockAchievement(nameAchievement + " 2");
                break;
            case < 800:
                UnlockAchievement(nameAchievement + " 3");
                break;
            case < 1600:
                UnlockAchievement(nameAchievement + " 4");
                break;
            case < 3200:
                UnlockAchievement(nameAchievement + " 5");
                break;
            case < 6400:
                UnlockAchievement(nameAchievement + " 6");
                break;
            default:
                break;

        }
    }
}
