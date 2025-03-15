using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent : ICharacterComponent
{
    public event Action<Character> OnCharacterDeath;

    public bool IsAlive { get; }

    public float MaxHealth { get; }

    public float Health { get; }

    public void SetDamage(float damage);
}
