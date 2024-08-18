using PlayFab.MultiplayerModels;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;
    public ShopItemSlot[] shopItemslots;
    public List<ItemProfile> itemList;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        int l = itemList.Count;
        for (int i = 0; i < l; ++i)
        {
            itemList[i].ItemID = i;
        }
    }
    public void InitShop()
    {
        Debug.Log("InitShop");
        foreach ( var itemSlot in shopItemslots ) 
        {
            itemSlot.InitItem();
        }
    }

    public void UpdateItemSlotList()
    {
        int l = shopItemslots.Length;
        for (int i = 0; i < l; i++)
        {
            shopItemslots[i].UpdateItemSlot();
            shopItemslots[i].CheckItemEquiping();
        }
    }
    public void UnequipItemAuto(GameObject gameObject)
    {
        for (int i = 0; i<shopItemslots.Length; i++)
        {
            Debug.Log(shopItemslots[i].ModelPrefab.name);
            if (shopItemslots[i].ModelPrefab.name == gameObject.name)
            {
                shopItemslots[i].UnequipItem();
            }
        }
    }



}
