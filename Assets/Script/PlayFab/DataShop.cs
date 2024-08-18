using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;
using Newtonsoft.Json;

using System;
using Unity.VisualScripting;
using System.Linq;


public class DataShop : MonoBehaviour
{
    public static DataShop instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void InitShopData()
    {
        ShopManager.instance.InitShop();
    }
    public void OnShopDataReceived(GetUserDataResult result, List<ItemProfile> itemList)
    {
        Debug.Log("OnShopDataReceived");
        // xu li data quest List
        ItemProfile[] allItemProfiles = Resources.LoadAll<ItemProfile>("Shop"); ;
        var itemListData = JsonConvert.DeserializeObject<List<ItemProfile>>(result.Data["Item"].Value);

        itemList = MapItemProfiles(allItemProfiles, itemListData);
     
        ShopManager.instance.UpdateItemSlotList();
    }

    private List<ItemProfile> MapItemProfiles(ItemProfile[] allItemProfiles, List<ItemProfile> itemDataList)
    {
        List<ItemProfile> itemProfiles = new List<ItemProfile>();

        foreach (var itemData in itemDataList)
        {

            // Tìm `ItemProfile` phù hợp từ tài sản
            ItemProfile itemProfile = allItemProfiles.FirstOrDefault(q => q.ItemID == itemData.ItemID);
            if (itemProfile != null)
            {
                itemProfile.ItemID = itemData.ItemID;
                //itemProfile.ItemDescription = itemData.ItemDescription;
                //itemProfile.ItemName = itemData.ItemName;
                itemProfile.ItemOwner = itemData.ItemOwner;
                itemProfile.ItemEquip = itemData.ItemEquip;
                //itemProfile.ItemValue= itemData.ItemValue;
                //itemProfile.ItemCost = itemData.ItemCost;
                // Khởi tạo các thuộc tính khác nếu cần
                itemProfiles.Add(itemProfile);
            }
            else
            {
                Debug.LogWarning($"QuestProfile with ID {itemData.ItemID} not found.");
            }
        }
        return itemProfiles;
    }

}


