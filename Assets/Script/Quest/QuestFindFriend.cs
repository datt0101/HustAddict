using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
public class QuestFindFriend : QuestObject
{
    public GameObject answerField;
    public TMP_Text questionText;
    [SerializeField] private InputHandle inputHandle;
    public override void HandleLogic()
    {
        answerField.SetActive(true);
        questionText.text = "Bạn đã tìm được " + questProfile.QuestTitle + " chưa? Hãy nhập đáp án vào đây nhé";
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && questProfile.QuestProgress == QuestProgress.Accepted)
        {
            if (inputHandle.IsSubmit)
                CheckAnswer();

        }
    }

    // Hàm xử lý khi người dùng nhập liệu
    public void CheckAnswer()
    {
        bool check = false;
       
        for (int i = 0; i < PlayFabAuth.instance.allIDList.Count; i++)
        {
            if (PlayFabAuth.instance.allIDList[i] == inputHandle.Input && inputHandle.Input != PlayerManager.instance.playerProfile.PlayerID)
            {
                QuestManager.instance.CompleteQuest(questProfile.QuestID);
                check = true;
                StartCoroutine(HideAnswerUI());
                inputHandle.IsSubmit = false;
                inputHandle.InputField.text = string.Empty;
                answerField.SetActive(false);
                break;
            }
        }
        if (!check)
        {
            Debug.Log("Tra loi sai");
            inputHandle.IsSubmit = false;
            inputHandle.InputField.text = string.Empty;
        }
    }
    IEnumerator HideAnswerUI()
    {
        yield return new WaitForSeconds(1);
        answerField.SetActive(false);
    }
}
