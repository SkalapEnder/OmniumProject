using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    private Character character;
    protected float currentHealth;
    protected float maxHealth;

    private bool isAlive;

    public bool IsAlive
    {
        get => isAlive;
        private set => isAlive = value;
    }

    public float MaxHealth => maxHealth;

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
                IsAlive = false;
            }
                
                
        } 
    }

    public void SetDamage(float damage)
    {
        Health -= damage;
    }

    public void Initialize(Character character)
    {
        this.character = character;
        maxHealth = character.CharacterData.DefaultMaxHealth;
        Health = maxHealth;
        IsAlive = true;
    }
}
