using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILiveComponent
{
    public float MaxHealth { get; }

    public float Health { get; }

    public void SetDamage(float damage);
}
