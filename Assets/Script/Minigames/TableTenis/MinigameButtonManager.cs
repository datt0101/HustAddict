using System;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MinigameButtonManager : MonoBehaviour
{
    public GameObject minigameMenu;
    public GameObject m_gameObject;
    public GameObject mainCamera;
    public GameObject mainPanel;
    //Minimap Menu
    public void OpenMenu()
    {
        Debug.Log("Click Pause");
        minigameMenu.SetActive(true);
        Time.timeScale = 0f;
    }
    public void CloseMenu()
    {
        minigameMenu.SetActive(false);
        Time.timeScale = 1f;
    }
    public void QuitMinigame()
    {
        Time.timeScale = 1f;
        m_gameObject.SetActive(false);
        mainPanel.SetActive(true);
        TimeManager.instance.gameObject.SetActive(true);
        mainCamera.SetActive(true); 
    }

}



