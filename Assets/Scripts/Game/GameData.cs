using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData")]
public class GameData : ScriptableObject
{
    [SerializeField] private int sessionTimeMinutes = 15;
    [SerializeField] private int timeBetweenItemSpawn = 30;
    [SerializeField] private float timeBetweenEnemySpawn = 1.5f;

    [Space(20)]
    [SerializeField] private float minEnemySpawnOffset = 7;
    [SerializeField] private float maxEnemySpawnOffset = 18;

    [Space(20)]
    [SerializeField] private float minItemSpawnOffset = 15;
    [SerializeField] private float maxItemSpawnOffset = 25;

    [Space(20)]
    [SerializeField] private int maxEntityNumberAtTime = 20;
    [SerializeField] private int maxTotalEntityNumber = 30;


    public int SessionTimeMinutes => sessionTimeMinutes;
    public int SessionTimeSeconds => sessionTimeMinutes * 60;
    public float TimeBetweenEnemySpawn => timeBetweenEnemySpawn;
    public float TimeBetweenItemSpawn => timeBetweenItemSpawn;
    public float MinEnemySpawnOffset => minEnemySpawnOffset;
    public float MaxEnemySpawnOffset => maxEnemySpawnOffset;

    public float MinItemSpawnOffset => minItemSpawnOffset;
    public float MaxItemSpawnOffset => maxItemSpawnOffset;

    public int MaxEntityNumberAtTime 
    {
        get { return maxEntityNumberAtTime; }
        set { maxEntityNumberAtTime = value; }
    }
    public int MaxTotalEntityNumber => maxTotalEntityNumber;
}
