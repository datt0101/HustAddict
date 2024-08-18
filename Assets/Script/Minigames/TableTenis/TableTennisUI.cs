using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TableTennisUI : MonoBehaviour
{
    public static TableTennisUI instance;
    [SerializeField] private TMP_Text m_playerScore;
    [SerializeField] private TMP_Text m_botScore;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void UpdateScore(int playerScore, int otherScore)
    {
        m_playerScore.text  = playerScore.ToString();
        m_botScore.text = otherScore.ToString();
    }
}
