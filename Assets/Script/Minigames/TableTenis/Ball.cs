using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Transform initialPosBall; // ball's initial position
    [SerializeField] private Transform bot;
    [SerializeField] private Transform initialPosBot;

    [SerializeField] private Transform player;
    [SerializeField] private Transform initialPosPlayer;
    bool checkPoint;
    [SerializeField] private int scorePlayer, scoreBot;
    [SerializeField] private int lastScore = 9;
    [SerializeField] private MinigameButtonManager minigameButton;
    [SerializeField] private int questID;
    [SerializeField] private GameObject completedText;

    public int QuestID { get => questID; set => questID = value; }

    //private void Awake()
    //{
    //    if (instance == null)
    //    {
    //        instance = this;
    //    }
    //    else
    //        Destroy(gameObject);
    //}
    private void OnEnable()
    {
        InitScore();
        TableTennisUI.instance.UpdateScore(scorePlayer, scoreBot);
        minigameButton.OpenMenu();
        Debug.Log(questID);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GroundCompetitor"))
        {
            checkPoint = true;
        }
        else if (other.CompareTag("GroundPlayer"))
        {
            checkPoint = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Ground")) 
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero; 
            this.transform.position = initialPosBall.position; // reset
            bot.position = initialPosBot.position;
            player.position = initialPosPlayer.position;
            if(checkPoint)
            {
                scorePlayer++;
                checkPoint = !checkPoint;
            }
            else
            {
                scoreBot++;
            }
            TableTennisUI.instance.UpdateScore(scorePlayer, scoreBot);

            if (scoreBot == lastScore)
            {
                completedText.SetActive(true);
                completedText.transform.localScale = Vector3.zero;
                completedText.GetComponentInChildren<TMP_Text>().text = "DEFEAT";
                completedText.transform.DOScale(1, .5f)
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(1f, () =>
                        {
                            completedText.transform.DOScale(0f, .5f)
                            .OnComplete(() =>
                            {
                                minigameButton.OpenMenu();
                                completedText.SetActive(false);
                                InitScore();
                                TableTennisUI.instance.UpdateScore(scorePlayer, scoreBot);

                            });
                        });
                    });
            }
            else if (scorePlayer == lastScore)
            {
                InitScore();
                completedText.SetActive(true);
                completedText.transform.localScale = Vector3.zero;
                completedText.GetComponentInChildren<TMP_Text>().text = "VICTORY";
                completedText.transform.DOScale(1, .5f)
                    .OnComplete(() =>
                    {
                        DOVirtual.DelayedCall(1f, () =>
                        {
                            completedText.transform.DOScale(0f, .5f)
                            .OnComplete(() =>
                            {
                                completedText.SetActive(false);
                                minigameButton.QuitMinigame();
                                QuestManager.instance.CompleteQuest(questID);
                            });
                        });
                    });            
            }
        }
    }
    public void InitScore()
    {
        scorePlayer = 0;
        scoreBot = 0;
    }

}
