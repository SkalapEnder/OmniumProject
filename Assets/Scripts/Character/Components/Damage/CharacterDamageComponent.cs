using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageComponent : IDamageComponent
{
    private Character character;
    private CharacterData characterData;

    private float _damage;
    private float _range;
    private float _cooldown;

    public float Damage => _damage;
    public float Cooldown => _cooldown;
    public float AttackRange => _range;

    public void Initialize(Character character)
    {
        this.character = character;
        characterData = character.CharacterData;

        _damage = characterData.DefaultDamage;
        _range = characterData.DefaultRange;
        _cooldown = characterData.TimeBetweenAttacks;
    }

    public void MakeDamage(Character characterTarget)
    {
        if (characterTarget.LiveComponent != null)
        {
            characterTarget.LiveComponent.SetDamage(Damage);
        }
    }
}
