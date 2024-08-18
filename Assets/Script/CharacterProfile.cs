using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/CharacterProfile")]

public class CharacterProfile : ScriptableObject
{
    public int ID;
    public int prize;
    public Status status;

}
    public enum Status
    {
        Purchased,
        Not_Purchased,
    }
