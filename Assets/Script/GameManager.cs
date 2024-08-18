
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private PlayerManager playerManager;
    [SerializeField] private AchievementManager achievementManager;
    [SerializeField] private QuestManager questManager;
    [SerializeField] private TimeManager timeManager;
    [SerializeField] private LightManager lightManager;
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private PlayFabData playfabManager;
    [SerializeField] private UIManager uiManager;
    private void Awake()
    {
        Application.targetFrameRate = 60;
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Reset()
    {
        LoadComponents();
    }
    protected virtual void LoadComponents()
    {
        this.audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        this.playerManager = GameObject.Find("PlayerManager").GetComponent<PlayerManager>();
        this.achievementManager = GameObject.Find("AchievementManager").GetComponent<AchievementManager>();
        this.questManager = GameObject.Find("QuestManager").GetComponent<QuestManager>();
        this.timeManager = GameObject.Find("TimeManager").GetComponent<TimeManager>(); 
        this.lightManager = GameObject.Find("LightManager").GetComponent<LightManager>();
        this.buttonManager = GameObject.Find("ButtonManager").GetComponent<ButtonManager>();
        this.playfabManager = GameObject.Find("PlayfabManager").GetComponent<PlayFabData>();
        this.uiManager = GameObject.Find("MainScreenCanvas").GetComponent<UIManager>();
       
    }
}
