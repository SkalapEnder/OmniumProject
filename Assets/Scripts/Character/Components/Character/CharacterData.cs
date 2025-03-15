using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    [SerializeField] private float damage;
    [SerializeField] private float rangeOfAttack;
    [SerializeField] private float timeBetweenAttacks;

    [SerializeField] private int scoreCost;
    [SerializeField] private Transform characterTransform;
    [SerializeField] private CharacterController characterController;


    // Attack Section
    public float DefaultDamage => damage;
    public float DefaultRange => rangeOfAttack;
    public float TimeBetweenAttacks => timeBetweenAttacks;

    // Move Section
    public float DefaultSpeed => speed;

    // Health Section
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
