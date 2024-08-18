
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class QuestLogButton : MonoBehaviour, ISelectHandler
{
    public QuestProfile questProfile;
    [SerializeField] private QuestUI questUI;
    [SerializeField] private TMP_Text buttonText;
    [SerializeField] private GameObject QuestLogButtonPanel; // Display cua nut

    public void OnSelect(BaseEventData eventData)
    {
        questUI.UpdateQuestInfo(questProfile);
        QuestManager.instance.questIDLogButton = questProfile.QuestID;
    }

    public void UpdateDataQuestLogButton(QuestProfile questProfile)
    {
        if (questProfile == null)
        {
            this.questProfile = null;
            this.buttonText.text = null;
            return;
        }
        this.questProfile = questProfile;
        this.buttonText.text = questProfile.QuestTitle;
        QuestLogButtonPanel.SetActive(true); // khi cập nhật data vào button, thì bật display của nút đó lên
    }

    public void EmptyDataQuestLogButton()
    {
        this.questProfile = null;
        this.buttonText.text = null;
        QuestLogButtonPanel.SetActive(false); // khi nhiệm vụ tương ứng vs button đó k còn, tắt display của nút đó đi
    }
}
