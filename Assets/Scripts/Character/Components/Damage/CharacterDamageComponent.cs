using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDamageComponent : IDamageComponent
{
    private Character character;
    private CharacterData characterData;

    private float _damage;
    private float _range;

    public float Damage => _damage;

    public float AttackRange => _range;

    public void Initialize(Character character)
    {
        this.character = character;
        characterData = character.CharacterData;

        _damage = characterData.DefaultDamage;
        _range = characterData.DefaultRange;
    }

    public void MakeDamage(Character characterTarget)
    {
        if(characterTarget.LiveComponent != null)
            characterTarget.LiveComponent.SetDamage(Damage);
    }
}
