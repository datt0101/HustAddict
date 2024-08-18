using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class QuestNPC : MonoBehaviour
{
    [SerializeField] private List<QuestProfile> NpcQuestList;
    [SerializeField] private GameObject chooseTypeQuestPanel;
    [SerializeField] private Button[] buttonList; // danh sach cac nut nhiem vu moi NPC quan li
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("touch NPC");
            chooseTypeQuestPanel.SetActive(true);
            UpdateQuestButtonList();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerInteract")
        {
            Debug.Log("touch NPC");
            chooseTypeQuestPanel.SetActive(true);
            UpdateQuestButtonList();
        }
    }
    private void Start()
    {
        sortNpcQuestList();
    }
    public void sortNpcQuestList() // sắp xếp lại danh sách theo các Type Quest để dễ thao tác dữ liệu hơn
    {
        NpcQuestList.Sort((quest1, quest2) => quest1.QuestType.CompareTo(quest2.QuestType));
    }
    public void UpdateQuestButtonList() // update danh sach cac nut nhiem vu cua npc do
    {
        // Reset lai danh sach cac nut để không bị lẫn với danh sách các nút của npc khác
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].gameObject.SetActive(false);
            buttonList[i].onClick.RemoveAllListeners();
        }

        int l = NpcQuestList.Count;
        if (l <= 0)
        {
            return;
        }
        int index = 0;
        for (int i = 0; i < l; i++) // cập nhật tất cả các TypeQuest mà NPC quản lý vào các nút
        {
            if (i == 0 || NpcQuestList[i].QuestType != NpcQuestList[i - 1].QuestType)
            {
                UpdateButton(buttonList[index], NpcQuestList[i]);
                index++;
            }
        }
    }
    public void UpdateButton(Button button, QuestProfile questProfile) // hàm thực hiện việc update các nút theo type quest tương ứng
    {
        button.gameObject.SetActive(true);
        button.GetComponentInChildren<TMP_Text>().text = questProfile.QuestType.ToString();
        string typeQuest = button.GetComponentInChildren<TMP_Text>().text.ToString();
        button.onClick.AddListener(() => TakeRandomQuest(typeQuest)); // gán sự kiện onClick
    }
    public void TakeRandomQuest(string typeQuest) // giao nhiệm vụ ngẫu nhiên dựa trên typeQuest mà button quản lí
    {
        List<QuestProfile> filteredQuestList = NpcQuestList.FindAll(quest => quest.QuestType.ToString() == typeQuest);
        if (filteredQuestList.Count > 0)
        {

            int randomIndex = Random.Range(0, filteredQuestList.Count);
            chooseTypeQuestPanel.SetActive(false);
            QuestManager.instance.AcceptQuest(filteredQuestList[randomIndex].QuestID);
        }
    }
}



