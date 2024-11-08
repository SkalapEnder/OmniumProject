using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalLiveComponent : ILiveComponent
{
    float ILiveComponent.MaxHealth { get => 1; }
    float ILiveComponent.Health { get => 1; }

    public void SetDamage(float damage)
    {
        Debug.Log("I am mortal!");
    }
}
