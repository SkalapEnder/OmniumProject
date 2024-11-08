using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageComponent 
{
    float Damage { get; }

    void MakeDamage(Character characterTarget);
}
