using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharacterLiveComponent : ILiveComponent
{
    private Character selfCharacter;
    protected float currentHealth;
    protected float maxHealth;
    protected float invicibleCooldown;

    public event Action<Character> OnCharacterDeath;
    public event Action<Character> OnCharacterHealthChange;

    public bool IsAlive =>
        currentHealth > 0;


    public float MaxHealth => maxHealth;
    public float InvicibleCooldown => invicibleCooldown;

    public float Health 
    { 
        get => currentHealth; 
        set 
        {
            if(Health > value)
            {
                ParticleSystem particle = GameManager.Instance.EffectsFactory.GetParticleSystem(EffectType.DamageEffect);
                particle.transform.position = selfCharacter.transform.position + Vector3.up;
                particle.Play();
            }

            currentHealth = value;
            if (currentHealth > MaxHealth)
                currentHealth = MaxHealth;
            OnCharacterHealthChange?.Invoke(selfCharacter);

            if (currentHealth <= 0)
            {
                currentHealth = 0;
                SetDeath();
            }
                
        } 
    }

    public void SetDamage(float damage)
    {
        OnCharacterHealthChange?.Invoke(selfCharacter);
        if (invicibleCooldown <= 0) 
            SetInvicibleCooldown(selfCharacter.CharacterData.InvicibleCooldown);
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
        invicibleCooldown = character.CharacterData.InvicibleCooldown;
        
    }

    public void SetInvicibleCooldown(float cooldown)
    {
        if (cooldown > 2f) cooldown = 2f;
        invicibleCooldown = cooldown;
    }

    public void SetInvicibleCooldown()
    {
        invicibleCooldown = selfCharacter.CharacterData.InvicibleCooldown;
    }
}
