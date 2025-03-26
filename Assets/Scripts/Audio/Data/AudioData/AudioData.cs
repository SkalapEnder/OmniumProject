using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class AudioData
{
    [SerializeField] private AudioClip audioClip;


    public AudioClip AudioClip => audioClip;
}

