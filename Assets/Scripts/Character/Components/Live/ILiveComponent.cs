using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent : ICharacterComponent
{
    public event Action<Character> OnCharacterHealthChange;
    public event Action<Character> OnCharacterDeath;

    public bool IsAlive { get; }

    public float MaxHealth { get; }

    public float Health { get; set; }

    public float InvicibleCooldown { get; }

    public void SetDamage(float damage);

    public void SetInvicibleCooldown(float cooldown);

    public void SetInvicibleCooldown();
}
