using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    private float currentHealth;

    private bool isDead;

    [SerializeField] private HUD hud;

    public bool IsDead
    {
        get => isDead;
        private set => isDead = value;
    }


    public float MaxHealth => 50;


    public float Health 
    { 
        get => currentHealth; 
        private set 
        {
            currentHealth = value;
            if (currentHealth > MaxHealth)
                currentHealth = MaxHealth;

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                SetDeath();
            }
        } 
    }

    public CharacterLiveComponent()
    {
        IsDead = false;
        Health = MaxHealth;
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
    }

    private void SetDeath()
    {
        IsDead = true;
    }
}
