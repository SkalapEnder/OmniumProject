using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    [SerializeField] private float skillPrice;
    private bool isUnlocked;

    public abstract void Initialize();

    public abstract void ApplyBoost();
}
