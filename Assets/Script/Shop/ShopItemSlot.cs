using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private GameObject modelPrefab;
    [SerializeField] private ItemProfile itemProfile;
    [SerializeField] private Transform parentBone;
    [SerializeField] private Sprite imageItem;
    [SerializeField] private ShopItemSlotUI shopItemSlotUI;

    public GameObject ModelPrefab { get => modelPrefab; set => modelPrefab = value; }
    public ItemProfile ItemProfile { get => itemProfile; set => itemProfile = value; }
    public Transform ParentBone { get => parentBone; set => parentBone = value; }
    public Sprite ImageItem { get => imageItem; set => imageItem = value; }
    public ShopItemSlotUI ShopItemSlotUI { get => shopItemSlotUI; set => shopItemSlotUI = value; }

    private void Start()
    {
        //UpdateItemSlot();
        //CheckItemEquiping();
    }
    public void InitItem()
    {
        Debug.Log("ShopItemSlot");
        itemProfile.ItemOwner = false;
        itemProfile.ItemEquip = false;
        UpdateItemSlot();
        CheckItemEquiping();
    }
    public void EquipItem()
    {
        if (ParentBone.childCount != 0)
        {
            Debug.Log("ShopIS" + ParentBone.GetChild(0).gameObject.name);
            ShopManager.instance.UnequipItemAuto(ParentBone.GetChild(0).gameObject);
            Destroy(ParentBone.GetChild(0).gameObject);
            
        }
        GameObject newItem = Instantiate(ModelPrefab, ParentBone);
        newItem.name = ModelPrefab.name;
        ItemProfile.ItemEquip = true;
        UpdateItemSlot();

        PlayFabData.instance.SaveData();
    }


    public void UnequipItem()
    {
        Destroy(ParentBone.GetChild(0).gameObject);
        ItemProfile.ItemEquip = false;
        UpdateItemSlot();

        PlayFabData.instance.SaveData();
    }
    public void BuyItem()
    {
        if (PlayerManager.instance.playerProfile.PlayerCredit >= ItemProfile.ItemCost)
        {
            PlayerManager.instance.playerProfile.PlayerCredit -= ItemProfile.ItemCost;
            ItemProfile.ItemOwner = true;
            UpdateItemSlot();
            ShopUI.instance.UpdateMoney();
            PlayFabData.instance.SaveData();
        }
    }

    public void UpdateItemSlot()
    {
        ShopItemSlotUI.UpdateItemSlotUI(ItemProfile, ImageItem);
    }
    public void UpdateItemSlotButton()
    {
        ShopItemSlotUI.UpdateItemSlotButton(ItemProfile);
    }

    public void CheckItemEquiping()
    {
        if (ItemProfile.ItemOwner && ItemProfile.ItemEquip)
        {
            EquipItem();
        }
    }
}
