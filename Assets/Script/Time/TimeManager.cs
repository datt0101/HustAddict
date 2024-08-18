using System;
using System.Threading;
using UnityEngine;
public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;
    [SerializeField] private LightTurning[] lightList;
    public static Action OnMinuteChanged;
    public static Action OnHourChanged;
    private float timer;
    private float minuteToRealTime = 2f; // 1 phut = 2 giay ngoai doi
    [SerializeField] private static int minute, hour;
    [SerializeField] private bool checkTurn = true;
    public static int Minute { get => minute; set => minute = value; }
    public static int Hour { get => hour; set => hour = value; }

    public void AddHour(int hour)
    {
       
        Hour = Hour + hour;
        if (Hour > 23)
            Hour = Hour - 24;
        
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        InitTime();
        timer = minuteToRealTime;
    }

    private void Update()
    {
        CalculateTime();
    }
    public void InitTime()
    {
        Debug.Log("Init Time");
        hour = 5;
        minute = 30;
        CheckTurnLight();
    }
    public void SetTime(int pHour, int pMinute)
    {
        hour = pHour;
        minute = pMinute;
        CheckTurnLight();
    }
    public void CalculateTime()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            minute++;
            OnMinuteChanged?.Invoke();
            if (minute >= 60)
            {
                hour++;
                minute = 0;
                OnHourChanged?.Invoke();
                CheckTurnLight();
                if (hour == 6)
                {
                    QuestManager.instance.ResetDailyQuest();
                }
                else if (hour > 23)
                {
                    hour = hour - 24;
                    InitTime();
                }
            }
            timer = minuteToRealTime;
        }
    }

    public void CheckTurnLight()
    {
        if ( (hour > 17 || hour < 6 ) && checkTurn)
        {
            foreach (var light in lightList)
            {
                light.TurnOnLight();
            }
            checkTurn = false;
        }
        else if ((hour >=6 && hour <=17) && !checkTurn)
        {
            foreach (var light in lightList)
            {
                light.TurnOffLight();
            }
            checkTurn = true;
        }
    }
 
}



