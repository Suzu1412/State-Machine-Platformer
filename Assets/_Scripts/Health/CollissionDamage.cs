using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollissionDamage : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out HealthSystem health))
        {
            health.GetHit(this.gameObject, 1);
        }

        if (collision.transform.parent.TryGetComponent(out HittableKnockback knockback))
        {
            knockback.GetHit(this.gameObject, 1);
        }
    }
}
