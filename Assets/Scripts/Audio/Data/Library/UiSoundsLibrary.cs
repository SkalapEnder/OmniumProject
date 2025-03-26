using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UiSoundLibrary", menuName = "Sound system/New Ui Sound Library", order = 1)]
public class UiSoundsLibrary : ScriptableObject
{
    [SerializeField] private UiSoundAudioData[] _sounds = default;


    public Dictionary<UiSoundType, AudioClip> GetUiSoundsDictionary()
    {
        Dictionary<UiSoundType, AudioClip> ambientsDictionary = new Dictionary<UiSoundType, AudioClip>();
        foreach (var audioData in _sounds)
            ambientsDictionary.Add(audioData.UiSoundType, audioData.AudioClip);

        return ambientsDictionary;
    }
}

