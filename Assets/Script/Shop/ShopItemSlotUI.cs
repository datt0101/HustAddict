using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlotUI : MonoBehaviour
{
    [Header("ItemSlot")]
    [SerializeField] private GameObject itemImage;
    [SerializeField] private TMP_Text itemCost;

    [Header("ItemSlot Button")]
    [SerializeField] private GameObject BuyButton;
    [SerializeField] private GameObject EquipButton;
    [SerializeField] private GameObject UnequipButton;

    public void UpdateItemSlotUI(ItemProfile itemProfile, Sprite itemSprite)
    {
        itemImage.GetComponent<Image>().sprite = itemSprite;
        this.itemCost.text = itemProfile.ItemCost.ToString();

        UpdateItemSlotButton(itemProfile);
    }

    public void UpdateItemSlotButton(ItemProfile itemProfile)
    {
        if (!itemProfile.ItemOwner)
        {
            TurnButton(BuyButton, EquipButton, UnequipButton);
        }
        else if (!itemProfile.ItemEquip)
        {
            TurnButton(EquipButton, BuyButton, UnequipButton);
        }
        else
        {
            TurnButton(UnequipButton, BuyButton, EquipButton);
        }
    }
    public void TurnButton(GameObject ButtonTurnOn, GameObject ButtonTurnOff1, GameObject ButtonTurnOff2)
    {
        ButtonTurnOn.SetActive(true);
        ButtonTurnOff1.SetActive(false);
        ButtonTurnOff2.SetActive(false);

    }
 
    
}
