using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Effects/Effects Library")]
public class EffectsLibrary : ScriptableObject
{
    [SerializeField] private EffectData[] effectsData;
    [SerializeField] private ProjectileData[] particleDatas;


    public EffectData[] EffectDatas => effectsData;
    public ProjectileData[] ProjectileDatas => particleDatas;



    [Serializable]
    public class EffectData
    {
        [SerializeField] private EffectType effectType;
        [SerializeField] private ParticleSystem effectPrefab;


        public EffectType EffectType => effectType;
        public ParticleSystem EffectPrefab => effectPrefab;
    }

    [Serializable]
    public class ProjectileData
    {
        [SerializeField] private EffectType effectType;
        [SerializeField] private ProjectileController projectilePrefab;


        public EffectType EffectType => effectType;
        public ProjectileController ProjectilePrefab => projectilePrefab;
    }
}

