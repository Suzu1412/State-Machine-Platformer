using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data/Melee", fileName = "WeaponData_")]
public class MeleeWeaponDataSO : BaseWeaponDataSO
{
    [SerializeField] private float attackRange = 2;

    public override void DrawWeaponGizmos(Vector3 origin, Vector3 direction)
    {
        Gizmos.DrawLine(origin, origin + direction * attackRange);
    }

    public override GameObject PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
        if (!TryAttack()) return null;

        RaycastHit2D[] hits = Physics2D.RaycastAll(origin.position, direction, attackRange, hittableMask);
        if (hits != null)
        {
            foreach(var hit in hits)
            {
                if (hit.collider.TryGetComponent(out IHittable hittable))
                {
                    hittable.GetHit(origin.gameObject, weaponDamage);
                }
            }

            /*
            foreach (var hittable in hit.collider.GetComponents<IHittable>())
            {
                hittable.GetHit(origin.gameObject, weaponDamage);
            }
            */
        }

        return null;

        /*
        RaycastHit2D hit = Physics2D.Raycast(origin.position, direction, attackRange, hittableMask);
        if (hit.collider != null)
        {
            foreach (var hittable in hit.collider.GetComponents<IHittable>())
            {
                hittable.GetHit(origin.gameObject, weaponDamage);
            }
        }
        */
    }

    protected override bool TryAttack()
    {
        return true;
    }

    public override bool CheckIfTargetInRange(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin.position, direction, attackRange * 0.8f, hittableMask);
        if (hit.collider != null)
        {
            return true;
        }
        return false;
    }
}
