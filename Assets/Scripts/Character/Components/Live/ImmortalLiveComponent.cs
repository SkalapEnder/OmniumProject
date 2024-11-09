using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImmortalLiveComponent : ILiveComponent
{
    private Character character;

    public float MaxHealth => 1;

    public float Health => 1;

    public bool IsAlive => true;

    public void Initialize(Character character)
    {
        this.character = character;
    }

    public void SetDamage(float damage)
    {
        Debug.Log("I am immortal!");
    }
}
