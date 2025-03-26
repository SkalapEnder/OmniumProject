using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AmbientLibrary", menuName = "Sound system/New Ambient Library", order = 1)]
public class AmbientLibrary : ScriptableObject
{
    [SerializeField] private AmbientAudioData[] _ambients = default;


    public Dictionary<AmbientType, AudioClip> GetAmbientDictionary()
    {
        Dictionary<AmbientType, AudioClip> ambientsDictionary = new Dictionary<AmbientType, AudioClip>();
        foreach (var ambientAudioData in _ambients)
            ambientsDictionary.Add(ambientAudioData.AmbientType, ambientAudioData.AudioClip);

        return ambientsDictionary;
    }
}

