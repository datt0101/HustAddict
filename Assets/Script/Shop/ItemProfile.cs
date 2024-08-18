using System.ComponentModel;
using UnityEngine;



[CreateAssetMenu(menuName = "ScriptableObjects/ItemProfile")]
public class ItemProfile : ScriptableObject
{
    [SerializeField] private int itemID;
    [SerializeField] private string itemName;
    [SerializeField] private string itemDescription;
    [SerializeField] private bool itemOwner;
    [SerializeField] private bool itemEquip;
    [SerializeField] private int  itemValue;
    [SerializeField] private int itemCost;
    [SerializeField] private ItemType itemType;
    public int ItemID { get => itemID; set => itemID = value; }
    public string ItemName { get => itemName; set => itemName = value; }
    public string ItemDescription { get => itemDescription; set => itemDescription = value; }
    public bool ItemOwner { get => itemOwner; set => itemOwner = value; }
    public bool ItemEquip { get => itemEquip; set => itemEquip = value; }
    public int ItemValue { get => itemValue; set => itemValue = value; }
    public int ItemCost { get => itemCost; set => itemCost = value; }
    internal ItemType ItemType { get => itemType; set => itemType = value; }
}

public enum ItemType
{
   Skin,
   Buff,
}