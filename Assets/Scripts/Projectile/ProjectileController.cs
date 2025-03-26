using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileController : MonoBehaviour
{
    [SerializeField]
    protected bool isDisableAfterMakeDamage;
    [SerializeField]
    protected float removeAfter;

    private Dictionary<Character, TargetData> targets = new Dictionary<Character, TargetData>();
    protected Character characterOwner;
    protected float projectileDamage;
    protected float projectileSpeed;
    protected Vector3 projectileDirection;
    protected float liveTime;


    public void Initialize(Character characterOwner, float damage, float speed, Vector3 direction)
    {
        this.characterOwner = characterOwner;
        projectileDamage = damage;
        projectileSpeed = speed;
        projectileDirection = direction;

        liveTime = removeAfter;

        gameObject.SetActive(true);
    }

    public void OnTriggerEnter(Collider other)
    {
        CheckCollision(other);
    }

    public void OnTriggerStay(Collider other)
    {
        CheckCollision(other);
    }

    protected abstract void OnMove();

    protected virtual void DisableProjectile()
    {
        targets.Clear();
        gameObject.SetActive(false);
    }

    private void CheckCollision(Collider other)
    {
        if (!other.gameObject.TryGetComponent<Character>(out Character targetCharacter))
            return;

        if (targetCharacter == characterOwner
            || targets.ContainsKey(targetCharacter))
            return;

        targetCharacter.LiveComponent.Health -= projectileDamage;
        if (isDisableAfterMakeDamage)
        {
            DisableProjectile();
        }
        else
        {
            TargetData target = new TargetData(projectileSpeed);
            targets.Add(targetCharacter, target);
        }
    }

    private void RemovingTargets()
    {
        if (isDisableAfterMakeDamage)
            return;

        List<Character> removingTargets = new List<Character>();
        foreach ((Character character, TargetData targetData) in targets)
        {
            if (targetData.targetTime <= 0)
                removingTargets.Add(character);
        }

        foreach (var removeTarget in removingTargets)
        {
            targets.Remove(removeTarget);
        }

        removingTargets.Clear();
    }

    private void Update()
    {
        OnMove();
        RemovingTargets();

        liveTime -= Time.deltaTime;
        if (liveTime <= 0)
        {
            DisableProjectile();
        }
    }


    protected class TargetData
    {
        public float targetTime;


        public TargetData(float targetTime)
        {
            this.targetTime = targetTime;
        }
    }
}

