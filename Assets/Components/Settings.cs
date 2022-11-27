using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public bool isFullScreen;
    public AudioMixer audioMixer;


    public void FullScreenToggle()
    {
        isFullScreen = !isFullScreen;
        Screen.fullScreen = isFullScreen;
    }

    public void AudioVolume(float sliderValue)
    {
        audioMixer.SetFloat("masterVolume", sliderValue);
    }
}
