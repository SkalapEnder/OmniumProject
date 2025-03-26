using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardProjectileController : ProjectileController
{
    [SerializeField] private Rigidbody rb;

    protected override void OnMove()
    {
        rb ??= GetComponent<Rigidbody>();
        rb.velocity = projectileDirection * projectileSpeed * Time.deltaTime;
    }
}


