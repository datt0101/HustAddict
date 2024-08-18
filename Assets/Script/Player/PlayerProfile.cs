using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/PlayerProfile")]
public class PlayerProfile : ScriptableObject
{
    [SerializeField] private string playerID; 
    [SerializeField] private int playerLevel; // Cap do cua nguoi choi
    [SerializeField] private string playerName;
    [SerializeField] private int playerStrength, playerIntelligence, playerSocial, playerKnowledge;// chi so 
    [SerializeField] private int playerCredit;
    public string PlayerID { get => playerID; set => playerID = value; }
    public int PlayerLevel { get => playerLevel; set => playerLevel = value; }
    public int PlayerCredit { get => playerCredit; set => playerCredit = value; }
    public string PlayerName { get => playerName; set => playerName = value; }
    public int PlayerStrength { get => playerStrength; set => playerStrength = value; }
    public int PlayerIntelligence { get => playerIntelligence; set => playerIntelligence = value; }
    public int PlayerSocial { get => playerSocial; set => playerSocial = value; }
    public int PlayerKnowledge { get => playerKnowledge; set => playerKnowledge = value; }

    public void SetPlayerData(string id, int level, string name, int strength, int intelligence, int social, int knowledge, int credit)
    {
        PlayerID = id;
        PlayerLevel = level;
        PlayerName = name;
        PlayerStrength = strength;
        PlayerIntelligence = intelligence;
        PlayerSocial = social;
        PlayerKnowledge = knowledge;
        PlayerCredit = credit;
    }

    public void AddStrength(int value)
    {
        Debug.Log("Add Strength");
        playerStrength += value;
    }
    public void AddIntelligence(int value)
    {
        Debug.Log("Add Intelligence");
        playerIntelligence += value;
    }
    public void AddSocial(int value)
    {
        Debug.Log("Add Social");
        playerSocial += value;
    }
    public void AddKnowledge(int value)
    {
        Debug.Log("Add Knowledge");
        playerKnowledge += value;
    }
    public void AddCredit(int value)
    {
        Debug.Log("Add Credit");
        playerCredit += value;
    }
}

