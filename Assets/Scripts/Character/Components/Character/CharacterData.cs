using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterData : MonoBehaviour
{
    [Header("Base settings")]
    [SerializeField] private float speed;
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private float invicibleCooldown;
    [SerializeField] private bool isImmortal;

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
    [SerializeField] private int xpCost;

    [SerializeField] private Animator animator;
    [SerializeField] private WeaponData starterWeaponData;
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
    public bool IsImmortal => isImmortal;
    public float InvicibleCooldown => invicibleCooldown;
    
    // Others Section
    public int ScoreCost => scoreCost;
    public int XPCost => xpCost;
    public Transform CharacterTransform => characterTransform;
    public CharacterController CharacterController => characterController;
    public Animator Animator => animator;
    public WeaponData StarterWeaponData => starterWeaponData;

}
