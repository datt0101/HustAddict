using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StudyMinigameUI : MonoBehaviour
{
    public static StudyMinigameUI instance;
    public GameObject studyMinigameCanvas;
    public GameObject gameOverPanel;
    public GameObject blackPanel;
    public GameObject blackOut;

    //Theory panel
    public TMP_Text nameTheoryText, theoryText;

    //Question panel
    public TMP_Text nameTheoryOfQuestionText,questionText, askText;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }
    public void GameOver()
    {
        blackPanel.SetActive(true);
        gameOverPanel.SetActive(true);
        gameOverPanel.transform.localScale = Vector3.zero;
        gameOverPanel.transform.DOScale(1f, .5f);
    }
    public void Quit()
    {
        blackOut.SetActive(true);
        blackOut.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0f);
        blackOut.GetComponent<Image>().DOColor(new Color(1f, 1f, 1f, 1f), 1f)
            .OnComplete(() =>
            {
                studyMinigameCanvas.SetActive(false);
            });
    }
    public void UpdateTheoryUI(LessonProfile lessonProfile)
    {
        nameTheoryText.text = lessonProfile.LessonName;
        theoryText.text = lessonProfile.LessonTheory + "\n\n" + lessonProfile.LessonFormula; 
    }

    public void UpdateQuestionUI(QuestionProfile questionProfile)
    {
        nameTheoryOfQuestionText.text = nameTheoryText.text;
        questionText.text = questionProfile.QuestionContent;
        askText.text = questionProfile.QuestionAsk;
    }

    public void EmptyUI()
    {
        nameTheoryText.text = null;
        theoryText.text = null;
        nameTheoryOfQuestionText.text = null;
        questionText.text = null;
        askText.text = null;
    }
}
