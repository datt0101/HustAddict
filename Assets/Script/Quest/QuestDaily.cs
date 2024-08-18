using System.Collections.Generic;
using UnityEngine;

public class QuestDaily : MonoBehaviour
{
    [SerializeField] private List<QuestProfile> NpcQuestList;

    private void Start()
    {
        TimeManager.OnHourChanged += HandleHourChanged;
    }

    private void HandleHourChanged()
    {
        if (TimeManager.Hour == 7 || TimeManager.Hour == 12)
        {
            // Phát nhiệm vụ khi giờ chuyển sang 7 hoặc 12h
            DistributeQuest();
        }
    }

    private void OnDestroy()
    {
        TimeManager.OnHourChanged -= HandleHourChanged;
    }

    public void DistributeQuest()
    {
        int max = NpcQuestList.Count;
        bool isAvailable = false;
        while (!isAvailable) // random ra nhiệm vụ có trạng thái available thì mới dừng, còn không sẽ random tiếp để kiếm nhiệm vụ tránh trường hợp random trúng nhiệm vụ player đã nhận
        {
            int randomNum = Random.Range(0, max); // random trong khoảng chứa TypeQuest 
            if (NpcQuestList[randomNum].QuestProgress == QuestProgress.Available)
            {
                QuestManager.instance.AcceptQuest(NpcQuestList[randomNum].QuestID);
                isAvailable = true;
            }
        }
    }
}


