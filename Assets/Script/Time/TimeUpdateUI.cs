
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
public class TimeUpdateUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    private void OnEnable()
    {
        TimeManager.OnHourChanged += UpdateTimeUI;
        TimeManager.OnMinuteChanged += UpdateTimeUI;
    }

    private void UpdateTimeUI()
    {
        //Debug.Log("UpdateTime");
        timeText.text = $"{TimeManager.Hour:00}:{TimeManager.Minute:00}";
    }
}



