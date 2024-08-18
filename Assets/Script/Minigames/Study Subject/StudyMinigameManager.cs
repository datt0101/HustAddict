
using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class StudyMinigameManager : MonoBehaviour
{
    [SerializeField] private LessonProfile lessonProfile;
    [SerializeField] private GameObject studyMinigame;
    [SerializeField] private StudyMinigameUI studyMinigameUI;
    [SerializeField] private AnswerButton[] answerButtons;
    [SerializeField] private GameObject lessonPanel;
    [SerializeField] private GameObject questionPanel;
    [SerializeField] private GameObject completedText;
    [SerializeField] private GameObject blackOut;
    private int numberQuestion = -1;

    private void Start()
    {
        //ActiveMinigame(lessonProfile);
    }
    public void ActiveMinigame(LessonProfile lessonProfile)
    {
        blackOut.SetActive(true);
        blackOut.GetComponent<Image>().color = new Color(0f, 0f, 0f, 1f);
        blackOut.GetComponent<Image>().DOColor(new Color(0, 0, 0, 0), 1f)
            .OnComplete(() =>
            {
                UIManager.instance.BlackIn();
                blackOut.SetActive(false);
                studyMinigame.SetActive(true);
                this.lessonProfile = lessonProfile;
                studyMinigameUI.UpdateTheoryUI(this.lessonProfile);
                lessonPanel.SetActive(true);
                questionPanel.SetActive(false);
                numberQuestion = -1;
            });
    }
    public void CloseMinigame()
    {
        DOVirtual.DelayedCall(0.3f, () =>
        {
            blackOut.SetActive(true);
            blackOut.GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            blackOut.GetComponent<Image>().DOColor(new Color(0f, 0f, 0f, 1f), 1f)
                .OnComplete(() =>
                {
                    UIManager.instance.BlackIn();
                    StudyMinigameUI.instance.EmptyUI();
                    this.lessonProfile = null;
                    lessonPanel.SetActive(false);
                    questionPanel.SetActive(false);
                    studyMinigame.SetActive(false);
                    //blackOut.SetActive(false);
                });
            //studyMinigame.SetActive(false);
            
        });
        
    }
    public void UpdateQuestionPanel(QuestionProfile questionProfile)
    {
        studyMinigameUI.UpdateQuestionUI(questionProfile);
        for (int i = 0;i<questionProfile.QuestionSelect.Length;i++) 
        {
            answerButtons[i].UpdateDataQuestionButton(questionProfile.QuestionSelect[i]);
        }
    }
    public void EmptyAnswerButton(QuestionProfile questionProfile)
    {
       
        for (int i = 0; i < questionProfile.QuestionSelect.Length; i++)
        {
            answerButtons[i].EmptyDataQuestionButton();
        }
    }
    public void NextButton()
    {
        numberQuestion++;
        if (numberQuestion > lessonProfile.QuestionProfileList.Length - 1)
        {
            CloseMinigame();
            return;
        }
        lessonPanel.SetActive(false);
        questionPanel.SetActive(true);
        if (numberQuestion > 0)
        {
            EmptyAnswerButton(lessonProfile.QuestionProfileList[numberQuestion - 1]);
        }
        UpdateQuestionPanel(lessonProfile.QuestionProfileList[numberQuestion]); 
    }
    public void BackButton()
    { 
        lessonPanel.SetActive(true);
        questionPanel.SetActive(false);
        numberQuestion--;
    }

    public void CheckTheAnswer(string choose)
    {
        if (lessonProfile.QuestionProfileList[numberQuestion].QuestionAnswer == choose)
        {
            PlayerStatsManager.instance.AddStats(lessonProfile.QuestionProfileList[numberQuestion].QuestionPoint, "Intelligence");
            completedText.GetComponentInChildren<TMP_Text>().text = "CORRECT!!";
            ActiveEffect();
        }
        else
        {
            completedText.GetComponentInChildren<TMP_Text>().text = "WRONG!!";
            ActiveEffect();
        }


    }
    public void ActiveEffect()
    {
        Debug.Log("Active effect");
        completedText.SetActive(true);
        completedText.transform.localScale = Vector3.zero;
        completedText.transform.DOScale(1, .5f)
            .OnComplete(() =>
            {
                DOVirtual.DelayedCall(1f, () =>
                {
                    completedText.transform.DOScale(0f, .5f)
                    .OnComplete(() =>
                    {
                        completedText.SetActive(false);
                        NextButton();
                    });
                });
            });
    }


}

