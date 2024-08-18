
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
public class AchievementUI : MonoBehaviour
{
    public static AchievementUI instance;
    [SerializeField] private GameObject unlockAchievementText;
    [SerializeField] private TMP_Text achievementTitleText;
    [SerializeField] private TMP_Text achievementDescriptionText;
    [SerializeField] private TMP_Text achievementRewardText;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    public void ShowAchievementEffect(string title)
    {
        ActiveEffect(title);
    }
    public void ActiveEffect(string title)
    {
        unlockAchievementText.GetComponentInChildren<TMP_Text>().text = "Mở khóa thành tựu " + title;
        Debug.Log("Active effect");
        unlockAchievementText.SetActive(true);
        unlockAchievementText.transform.localScale = Vector3.zero;
        unlockAchievementText.transform.DOScale(1, .5f)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(1.5f, () =>
                {
                    unlockAchievementText.transform.DOScale(0f, .5f)
                    .OnComplete(() =>
                    {
                        unlockAchievementText.SetActive(false);
                    });
                });
            });
    }
    public void ShowPanelInfo(AchievementProfile achievementProfile)
    {
        achievementTitleText.text = achievementProfile.AchievementName;
        achievementDescriptionText.text = achievementProfile.AchievementDescription;
        achievementRewardText.text = "Phần thưởng: " + achievementProfile.AchievementReward.ToString() + " xu";
    }
}
