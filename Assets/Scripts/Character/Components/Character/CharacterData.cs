using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [Space(10)]
    [Header("Range settings")]
    [SerializeField] private float viewDistance;
    [SerializeField] private float rangeOfAttack;

    [Space(10)]
    [Header("Attack settings")]
    [SerializeField] private float damage;
    [SerializeField] private float timeBetweenAttacks;

    [Space(10)]
    [Header("Other settings")]
    [SerializeField] private int scoreCost;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;

    // Range Section
    public float DefaultRange => rangeOfAttack;
    public float DefaultView => viewDistance;

    // Attack Section
    public float DefaultDamage => damage;
    public float TimeBetweenAttacks => timeBetweenAttacks;

    // Base Section
    public float DefaultSpeed => speed;
    public float DefaultHealth => health;
    public float DefaultMaxHealth => maxHealth;
    
    // Others Section
    public int ScoreCost => scoreCost;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController
    { 
        get { return characterController; } 
    }
}
