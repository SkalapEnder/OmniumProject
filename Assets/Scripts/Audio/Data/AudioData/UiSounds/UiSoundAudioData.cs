using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class UiSoundAudioData : AudioData
{
    [SerializeField] private UiSoundType _uiSoundType;


    public UiSoundType UiSoundType => _uiSoundType;
}

