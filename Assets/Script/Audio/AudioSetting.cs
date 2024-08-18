using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AudioSetting  : MonoBehaviour
{
    [SerializeField] private Slider audioSlider;

    public void SetAudioVolume()
    {
        float volume = audioSlider.value;
        AudioManager.instance.audioSource.volume = volume;
    }
}
