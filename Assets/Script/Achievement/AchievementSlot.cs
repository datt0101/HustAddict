using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSlot : MonoBehaviour
{
        [SerializeField] private AchievementProfile achievementProfile;
        [SerializeField] private Image achievementFrame;
        [SerializeField] private Image achievementImage;
        public AchievementProfile AchievementProfile { get => achievementProfile; set => achievementProfile = value; }

        public void UnlockAchievementSlot()
        {
            Debug.Log("Color");
            Color color = achievementImage.color;
            color.a = 100f;
            achievementImage.color = color;
            achievementFrame.color = color;
        }
}
