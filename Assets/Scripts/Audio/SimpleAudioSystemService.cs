using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SimpleAudioSystemService : AudioSystemService
{
    private const float DB_MINIMUM = -80f;
    private const float DB_MAXIMUM = 0f;


    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private AudioLibrary _audioLibrary;

    private Dictionary<AudioSystemType, AudioMixerGroup> _mixerGroups;


    public override Dictionary<AmbientType, AudioClip> AmbientsDictionary { get; protected set; }
    public override Dictionary<SoundType, AudioClip> SoundsDictionary { get; protected set; }
    public override Dictionary<UiSoundType, AudioClip> UiSoundsDictionary { get; protected set; }

    private float lastVolume = 0f;
    private bool isMasterEnabled;
    public bool IsMasterEnabled => isMasterEnabled;

    public override void Initialize()
    {
        AmbientsDictionary = _audioLibrary.AmbientLibrary.GetAmbientDictionary();
        SoundsDictionary = _audioLibrary.SoundsLibrary.GetSoundsDictionary();
        UiSoundsDictionary = _audioLibrary.UiSoundsLibrary.GetUiSoundsDictionary();
        _mixerGroups = _audioLibrary.AudioMixerGroupDictionary;

        isMasterEnabled = true;

        base.Initialize();
    }

    public override AudioSource CreateAudioSource(GameObject audioSourceParent, AudioSystemType type)
    {
        AudioSource audioSource = audioSourceParent.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.outputAudioMixerGroup = _mixerGroups[type];

        return audioSource;
    }

    public override void SetVolume(AudioSystemType type, float volume)
    {
        if (!isInitialized)
            return;

        var group = _mixerGroups[type];
        int masterCoef = isMasterEnabled ? 1 : 0;

        if (!group.audioMixer.SetFloat(type.ToString(),
                Mathf.Lerp(DB_MINIMUM, DB_MAXIMUM, volume * masterCoef)))
        {
            Debug.LogError($"Not found {type.ToString()} audio type!");
        }
    }

    public override void SetVolume(AudioSystemType type, bool status)
    {
        SetVolume(type, status && isMasterEnabled ? 1 : 0);
    }

    public override AudioMixerGroup GetAudioMixerGroup(AudioSystemType type)
    {
        return _audioMixer.FindMatchingGroups(type.ToString())[0];
    }

    public AudioClip GetRandomSound(SoundType type)
    {
        if (!SoundsDictionary.TryGetValue(type, out AudioClip clip) || clip == null)
        {
            Debug.LogError($"No sounds found for type: {type}");
            return null;
        }

        // If multiple variations exist, get a random one
        List<AudioClip> clips = SoundsDictionary
            .Where(kv => kv.Key == type)
            .Select(kv => kv.Value)
            .ToList();

        if (clips.Count == 0)
        {
            Debug.LogError($"No clips available for {type}");
            return null;
        }

        return clips[Random.Range(0, clips.Count)];
    }

    public void ToggleAmbientSound(AmbientType type, AudioSource audioSource, bool isStart)
    {
        if (!AmbientsDictionary.TryGetValue(type, out AudioClip clip) || clip == null)
        {
            Debug.LogError($"Ambient sound {type} not found!");
            return;
        }

        audioSource.clip = clip;
        audioSource.loop = true;
        if(isStart) audioSource.Play();
        else audioSource.Stop();
    }

    public override float GetVolume(AudioSystemType type)
    {
        if (!isInitialized)
            return DB_MINIMUM;

        var group = _mixerGroups[type];
        float volume; 
        group.audioMixer.GetFloat(type.ToString(), out volume);

        return volume;
    }

    public override void ToggleMute()
    {
        var audioMixer = _mixerGroups[AudioSystemType.Master];

        if (isMasterEnabled) // Muting the sound
        {
            audioMixer.audioMixer.GetFloat(AudioSystemType.Master.ToString(), out lastVolume);
            audioMixer.audioMixer.SetFloat(AudioSystemType.Master.ToString(), DB_MINIMUM);
        }
        else // Unmuting the sound
        {
            audioMixer.audioMixer.SetFloat(audioMixer.audioMixer.ToString(), lastVolume);
        }

        isMasterEnabled = !isMasterEnabled;
    }
}

