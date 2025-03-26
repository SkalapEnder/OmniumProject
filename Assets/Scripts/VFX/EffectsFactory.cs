using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsFactory : MonoBehaviour
{
    [SerializeField] private EffectsLibrary effectsLibrary;

    private Dictionary<EffectType, List<ProjectileController>> projectiles = new Dictionary<EffectType, List<ProjectileController>>();
    private Dictionary<EffectType, List<ParticleSystem>> effects = new Dictionary<EffectType, List<ParticleSystem>>();


    public ProjectileController GetProjectile(EffectType effectType)
    {
        if (!projectiles.ContainsKey(effectType))
        {
            projectiles.Add(effectType, new List<ProjectileController>());
            return CreateProjectile(effectType);
        }

        ProjectileController projectile = null;
        foreach (var createdProjectile in projectiles[effectType])
        {
            if (createdProjectile.gameObject.activeSelf)
                continue;

            projectile = createdProjectile;
        }

        return projectile == null
            ? CreateProjectile(effectType)
            : projectile;
    }

    private ProjectileController CreateProjectile(EffectType effectType)
    {
        ProjectileController projectile = null;

        foreach (var projectileData in effectsLibrary.ProjectileDatas)
        {
            if (projectileData.EffectType != effectType)
                continue;

            projectile = GameObject.Instantiate<ProjectileController>(projectileData.ProjectilePrefab, this.gameObject.transform);
            projectiles[effectType].Add(projectile);
            return projectile;
        }

        Debug.LogError($"Unknown projectile type {effectType}");
        return null;
    }

    // Particle
    public ParticleSystem GetParticleSystem(EffectType effectType)
    {
        if (!effects.ContainsKey(effectType))
        {
            effects.Add(effectType, new List<ParticleSystem>());
            return CreateParticle(effectType);
        }
        ParticleSystem particle = null;
        foreach (ParticleSystem effect in effects[effectType])
        {
            if (effect.isPlaying)
                continue;

            particle = effect;
        }
        return particle == null
            ? CreateParticle(effectType)
            : particle;
    }

    private ParticleSystem CreateParticle(EffectType effectType)
    {
        ParticleSystem particle = null;
        foreach (var effectData in effectsLibrary.EffectDatas)
        {
            if (effectData.EffectType != effectType)
                continue;
             
            particle = GameObject.Instantiate<ParticleSystem>(effectData.EffectPrefab, this.gameObject.transform);
            effects[effectType].Add(particle);
            return particle;
        }
        Debug.LogError($"Unknown effect type {effectType}");
        return null;
    }

    public void RemoveAll()
    {
        //// Destroy all particle system clones
        //foreach (var effectList in effects.Values)
        //{
        //    foreach (var effect in effectList)
        //    {
        //        if (effect != null)
        //        {
        //            Destroy(effect.gameObject);
        //        }
        //    }
        //}
    }
}

