
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


public class AnswerButton : MonoBehaviour
{
    [SerializeField] private TMP_Text answerText;
    [SerializeField] private GameObject QuestionButtonPanel; // Display cua nut
    [SerializeField] private StudyMinigameManager studyMinigameManager;
    //public void OnSelect(BaseEventData eventData)
    //{
    //    Debug.Log("On Select");
    //    if (UnityEngine.EventSystems.EventSystem.current.alreadySelecting == true) 
    //    {
    //        Debug.Log("true");
    //        EventSystem.current.SetSelectedGameObject(null); 
    //    }

    //    StudyMinigameManager.instance.CheckTheAnswer(answerText.text);

    //}

    public void ChooseAnswer()
    {
        studyMinigameManager.CheckTheAnswer(answerText.text);
    }

    public void UpdateDataQuestionButton(string answerText)
    {
        this.answerText.text = answerText;
        QuestionButtonPanel.SetActive(true); // khi cập nhật data vào button, thì bật display của nút đó lên
    }

    public void EmptyDataQuestionButton()
    {
        this.answerText.text = null;
        QuestionButtonPanel.SetActive(false); // khi nhiệm vụ tương ứng vs button đó k còn, tắt display của nút đó đi
    }
}

