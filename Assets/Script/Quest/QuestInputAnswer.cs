using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.Users;
using UnityEngine.UI;

public class QuestInputAnswer : QuestObject
{
    [SerializeField] private string answer;
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
        Debug.Log("input: " + inputHandle.Input + "-- Answer: " + answer);
        if (inputHandle.Input == answer)
        {
            Debug.Log("Tra loi dung");
            QuestManager.instance.CompleteQuest(questProfile.QuestID);
            
            StartCoroutine(HideAnswerUI());
            inputHandle.IsSubmit = false;
            inputHandle.InputField.text = string.Empty;
            answerField.SetActive(false);
        }
        else
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
