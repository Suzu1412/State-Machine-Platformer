using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class HittableKnockback : MonoBehaviour, IHittable
{
    private int knockbackDirection;
    public int KnockbackDirection => knockbackDirection;    
    public event Action OnHitKnockback;

    public void GetHit(GameObject opponent, int weaponDamage)
    {
        if (transform.position.x < opponent.transform.position.x)
        {
            knockbackDirection = -1;
        }
        else
        {
            knockbackDirection = 1;
        }
        OnHitKnockback?.Invoke();
    }
}
