using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AmbientAudioData : AudioData
{
    [SerializeField] private AmbientType _ambientType;


    public AmbientType AmbientType => _ambientType;
}

