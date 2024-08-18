using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectObjectsParent : MonoBehaviour
{
    private int numbersOfObject;
    private int count;
    public QuestProfile quest;
    private void Start()
    {
        numbersOfObject = transform.childCount;
        count = numbersOfObject;
    }
    public void Collected()
    {
        count--;
        if (count == 0)
        {
            Invoke("RespawnChild", 5f);
            QuestManager.instance.CompleteQuest(quest.QuestID);       
        }
    }

    public void RespawnChild()
    {
        for (int i = 0; i < numbersOfObject; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void OnApplicationQuit()
    {
        RespawnChild();
    }
}
