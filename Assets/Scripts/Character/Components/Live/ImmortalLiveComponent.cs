using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalLiveComponent : ILiveComponent
{
    public float MaxHealth => 100;

    public float Health => 100;

    public bool IsAlive => true;

    public float InvicibleCooldown => 1;

    float ILiveComponent.Health { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public event Action<Character> OnCharacterDeath;
    public event Action<Character> OnCharacterHealthChange;
    public void Initialize(Character character)
    {

    }

    public void SetDamage(float damage)
    {
        //Debug.Log("I am immortal!");
    }

    public void SetInvicibleCooldown(float cooldown)
    {
        
    }

    public void SetInvicibleCooldown()
    {
        
    }

    public void Heal(float heal)
    {

    }
}
