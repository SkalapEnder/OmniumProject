using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(menuName = "Sound system/Sound Library")]
public class AudioLibrary : ScriptableObject
{
    [SerializeField] private AudioMixerGroupData[] _audioMixerGroupDatas;

    [Space(5)]
    [SerializeField] private AmbientLibrary _ambientLibrary;
    [SerializeField] private UiSoundsLibrary _uiSoundsLibrary;
    [SerializeField] private SoundsLibrary _soundsLibrary;


    public AmbientLibrary AmbientLibrary => _ambientLibrary;

    public UiSoundsLibrary UiSoundsLibrary => _uiSoundsLibrary;

    public SoundsLibrary SoundsLibrary => _soundsLibrary;

    public Dictionary<AudioSystemType, AudioMixerGroup> AudioMixerGroupDictionary
    {
        get
        {
            Dictionary<AudioSystemType, AudioMixerGroup> newDict = new();
            foreach (var data in _audioMixerGroupDatas)
                newDict.Add(data.AudioType, data.AudioMixerGroup);
            return newDict;
        }
    }


    [Serializable]
    public class AudioMixerGroupData
    {
        [SerializeField] private AudioSystemType _audioType;
        [SerializeField] private AudioMixerGroup _audioMixerGroup;


        public AudioSystemType AudioType => _audioType;

        public AudioMixerGroup AudioMixerGroup => _audioMixerGroup;
    }
}

