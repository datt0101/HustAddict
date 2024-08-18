using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public GameObject RMCanvas;
    public GameObject gameOverPanel;
    public GameObject blackPanel;
    public GameObject blackOut;
    public TextMeshProUGUI scoreText;

    public static UI instance;
    private void Awake()
    {
        instance = this;
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
                RMCanvas.SetActive(false);
            });
    }
    public void SetScoreText()
    {
        scoreText.text = "Score: ";
    }
}
