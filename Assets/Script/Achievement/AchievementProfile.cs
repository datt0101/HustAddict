using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/AchievementProfile")]
public class AchievementProfile : ScriptableObject
{
    [SerializeField] private int achievementID;
    [SerializeField] private string achievementName;
    [SerializeField] private string achievementDescription;
    [SerializeField] private int achievementReward;
    [SerializeField] private bool  achievementStatus;

    public string AchievementName { get => achievementName; set => achievementName = value; }
    public string AchievementDescription { get => achievementDescription; set => achievementDescription = value; }
    public int AchievementReward { get => achievementReward; set => achievementReward = value; }
    public bool AchievementStatus { get => achievementStatus; set => achievementStatus = value; }
    public int AchievementID { get => achievementID; set => achievementID = value; }
}

