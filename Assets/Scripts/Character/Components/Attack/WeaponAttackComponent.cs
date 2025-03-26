using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttackComponent : IAttackComponent
{
    private Character character;
    private WeaponData currentWeaponData;
    private float timeBetweenAttack;

    public float Damage => currentWeaponData.WeaponDamage;
    public float AttackRange => currentWeaponData.AttackRange;

    private EffectsFactory EffectsFactory =>
        GameManager.Instance.EffectsFactory;


    public void MakeAttack()
    {
        if (timeBetweenAttack > 0
            || character.Target == null)
            return;

        float distance = Vector3.Distance(
            character.CharacterData.CharacterTransform.position,
            character.Target.CharacterData.CharacterTransform.position);

        if (distance > currentWeaponData.AttackRange)
            return;

        character.AnimationComponent.SetTrigger("Attack");
        timeBetweenAttack = currentWeaponData.TimeBetweenAttack;
        PlayGunShotSound();
        //character.CameraShake();
        //character.Target.LiveComponent.SetDamage(currentWeaponData.WeaponDamage);

        var projectile = EffectsFactory.GetProjectile(currentWeaponData.ProjectileTypeEffect);
        projectile.transform.position = character.transform.position + character.transform.forward + Vector3.up;

        projectile.transform.rotation = character.CharacterData.CharacterTransform.rotation;
        projectile.Initialize(this.character, Damage, 9000,
            (character.Target.transform.position - character.transform.position).normalized);
    }

    public void OnUpdate()
    {
        if (timeBetweenAttack > 0)
        {
            timeBetweenAttack -= Time.deltaTime;
        }
    }

    public void Initialize(Character character)
    {
        this.character = character;
        currentWeaponData = character.CharacterData.StarterWeaponData;
        timeBetweenAttack = 0;
    }

    private void PlayGunShotSound()
    {
        if (GameManager.Instance.AudioService == null)
        {
            Debug.LogError("GameManager.AudioService is NULL!");
            return;
        }

        AudioClip clip = GameManager.Instance.AudioService.GetRandomSound(SoundType.GunSound);
        if (clip == null) return;

        character.AudioSource.pitch = Random.Range(0.96f, 1.07f);
        character.AudioSource.PlayOneShot(clip);
    }
}

