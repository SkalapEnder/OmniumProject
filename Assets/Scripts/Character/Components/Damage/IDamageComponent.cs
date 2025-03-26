using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageComponent : ICharacterComponent
{
    public float Damage { get; }
    public float AttackRange { get; }
    public float Cooldown { get;  }

    void MakeDamage(Character characterTarget);
}
