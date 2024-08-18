using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    //am thanh background
    public AudioSource audioSource;

    // Khai bao các âm thanh
    public AudioClip jumpingSound; // nhay
    public AudioClip movingSound; // di chuyen
    public AudioClip completeQuestSound; // hoan thanh nv
    public AudioClip acceptQuestSound; // nhan nhiem vu
    public AudioClip unlockAchievementSound; // mo khoa thanh tuu
    public AudioClip levelUpSound; // len level
    public AudioClip addStatsSound; // tang chi so
    public AudioClip sprintSound; // di chuyen tang toc
    public AudioClip buttonSound1; // button
    public AudioClip buttonSound2; // button
    public AudioClip notifySound; // button
    public AudioClip thumpSound; // 
    public AudioClip giveUpSound; // 
    public AudioClip collectItem; 
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        PlayBackgroundMusic();
        audioSource.volume = 0.5f;
    }
    private void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("null");
        }
    }
    public void PlayBackgroundMusic()
    {
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBackgroundMusic()
    {
        audioSource.Stop();
    }
    public void PlayJumpingSound()
    {
        PlaySound(jumpingSound);
    }
    public void PlayMovingSound()
    {
        PlaySound(movingSound);
    }
    public void PlaySprintSound()
    {
        PlaySound(sprintSound);
    }
    public void PlayCompleteQuestSound()
    {
        PlaySound(completeQuestSound);
    }
    public void PlayAcceptQuestSound()
    {
        PlaySound(acceptQuestSound);
    }
    public void PlayUnlockAchievementSound()
    {
        PlaySound(unlockAchievementSound);
    }
    public void PlayAddStatsSound()
    {
        PlaySound(addStatsSound);
    }
    public void PlayLevelUpSound()
    {
        PlaySound(levelUpSound);
    }
    public void PlayButtonSound1()
    {
        PlaySound(buttonSound1);
    }
    public void PlayButtonSound2()
    {
        PlaySound(buttonSound2);
    }
    public void PlayNotifySound()
    {
        PlaySound(notifySound);
    }
    public void PlayThumpSound()
    {
        PlaySound(thumpSound);
    }
    public void PlayGiveUpSound()
    {
        PlaySound(giveUpSound);
    }
    public void PlayCollectItem()
    {
        PlaySound(collectItem);
    }
}

