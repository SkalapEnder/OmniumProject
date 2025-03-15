using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private int sessionTimeMinutes = 15;
    [SerializeField] private float timeBetweenEnemySpawn = 1.5f;
    [Space(20)]
    [SerializeField] private float minEnemySpawnOffset = 7;
    [SerializeField] private float maxEnemySpawnOffset = 18;
    [SerializeField] private int maxEntityNumberAtTime = 20;


    public int SessionTimeMinutes => sessionTimeMinutes;
    public int SessionTimeSeconds => sessionTimeMinutes * 60;
    public float TimeBetweenEnemySpawn => timeBetweenEnemySpawn;
    public float MinEnemySpawnOffset => minEnemySpawnOffset;
    public float MaxEnemySpawnOffset => maxEnemySpawnOffset;
    public int MaxEntityNumberAtTime 
    {
        get { return maxEntityNumberAtTime; }
        set { maxEntityNumberAtTime = value; }
    }
}
