using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundLibrary", menuName = "Sound system/New Sound Library", order = 1)]
public class SoundsLibrary : ScriptableObject
{
    [SerializeField] private SoundAudioData[] _sounds = default;


    public Dictionary<SoundType, AudioClip> GetSoundsDictionary()
    {
        Dictionary<SoundType, AudioClip> ambientsDictionary = new Dictionary<SoundType, AudioClip>();
        foreach (var audioData in _sounds)
            ambientsDictionary.Add(audioData.SoundType, audioData.AudioClip);

        return ambientsDictionary;
    }
}

