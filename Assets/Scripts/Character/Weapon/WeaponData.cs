using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponData : ScriptableObject
{
    [SerializeField]
    private float weaponDamage;
    [SerializeField]
    private float timeBetweenAttack;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private EffectType shellTypeEffect;
    [SerializeField]
    private EffectType projectileTypeEffect;


    public float WeaponDamage => weaponDamage;
    public float TimeBetweenAttack => timeBetweenAttack;
    public float AttackRange => attackRange;
    public EffectType ProjectileTypeEffect => projectileTypeEffect;
    public EffectType ShellTypeEffect => shellTypeEffect;
}
