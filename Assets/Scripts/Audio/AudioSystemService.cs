using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public abstract class AudioSystemService : MonoBehaviour, IAudioSystemService
{
    private static AudioSystemService _instance;

    protected bool isInitialized = false;


    public static AudioSystemService Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<AudioSystemService>();
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public abstract Dictionary<AmbientType, AudioClip> AmbientsDictionary { get; protected set; }
    public abstract Dictionary<SoundType, AudioClip> SoundsDictionary { get; protected set; }
    public abstract Dictionary<UiSoundType, AudioClip> UiSoundsDictionary { get; protected set; }


    public virtual void Initialize() =>
        isInitialized = true;

    public abstract void SetVolume(AudioSystemType type, float volume);

    public abstract void SetVolume(AudioSystemType type, bool status);

    public abstract float GetVolume(AudioSystemType type);

    public abstract void ToggleMute();

    public abstract AudioMixerGroup GetAudioMixerGroup(AudioSystemType type);

    public abstract AudioSource CreateAudioSource(GameObject audioSourceParent, AudioSystemType type);

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
            Initialize();
        }
        else
        {
            Destroy(this);
        }
    }
}

