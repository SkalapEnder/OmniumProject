using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    public bool isSoundEnabled = true;
    private float keepSoundVolume = 1.0f;

    public void Initialize()
    {
        
    }

    public void SetVolume(float volume)
    {

        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("Volume", volume); // Save volume setting
        PlayerPrefs.Save();
        Debug.Log(volume);
    }

    public void ToggleVolume()
    {
        isSoundEnabled = !isSoundEnabled;
        AudioListener.volume = isSoundEnabled ? keepSoundVolume : 0;
    }
}
