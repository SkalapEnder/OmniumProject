using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SoundAudioData : AudioData
{
    [SerializeField] private SoundType soundType;


    public SoundType SoundType => soundType;
}

