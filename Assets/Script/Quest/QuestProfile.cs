using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/QuestProfile")]
public class QuestProfile:ScriptableObject
{
    [SerializeField] private int questLevel; // quest theo level cua sinh vien ( 1,2,3,4)
    [SerializeField] private string questTitle; 
    [SerializeField] private string questDescription; // mo ta nhiem vu
    [SerializeField] private int questID; 
    [SerializeField] private string questSummary;// tom tat nv
    [SerializeField] private int questPoint; // diem thuong
    [SerializeField] private int questAnswer; 
    [SerializeField] private QuestProgress questProgress;
    [SerializeField] private QuestType questType; // the loai nhiem vu
    [SerializeField] private QuestReward questReward;
    public string QuestTitle { get => questTitle; set => questTitle = value; }
    public string QuestDescription { get => questDescription; set => questDescription = value; }
    public int QuestID { get => questID; set => questID = value; }
    public string QuestSummary { get => questSummary; set => questSummary = value; }
    public int QuestPoint { get => questPoint; set => questPoint = value; }
    public int QuestAnswer { get => questAnswer; set => questAnswer = value; }
    public int QuestLevel { get => questLevel; set => questLevel = value; }
    internal QuestProgress QuestProgress { get => questProgress; set => questProgress = value; }
    internal QuestType QuestType{ get => questType; set => questType = value; }
    internal QuestReward QuestReward { get => questReward; set => questReward = value; }
}

enum QuestProgress
{
    Not_Available,
    Available,
    Accepted,
    Complete
}

enum QuestType
{
    Study,
    Sport,
    Minigame,
    Discover,
    Main,
    Input,
    Other
}

enum QuestReward
{
    Strength,
    Intelligence,
    Social,
    Knowledge
}

