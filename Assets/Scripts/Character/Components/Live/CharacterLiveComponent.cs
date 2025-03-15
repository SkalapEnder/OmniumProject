using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CharacterLiveComponent : ILiveComponent
{
    private Character selfCharacter;
    protected float currentHealth;
    protected float maxHealth;

    private bool isAlive;

    public event Action<Character> OnCharacterDeath;

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
                SetDeath();
            }
                
        } 
    }

    public void SetDamage(float damage)
    {
        Debug.Log("Ouch! " + damage + " " + (Health - damage));
        Health -= damage;
    }

    public void SetDeath()
    {
        OnCharacterDeath?.Invoke(selfCharacter);
    }

    public void Initialize(Character character)
    {
        this.selfCharacter = character;
        maxHealth = character.CharacterData.DefaultMaxHealth;
        Health = maxHealth;
        IsAlive = true;
    }
}
