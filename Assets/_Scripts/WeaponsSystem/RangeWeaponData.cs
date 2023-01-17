using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data/Range", fileName = "WeaponData_")]
public class RangeWeaponData : BaseWeaponDataSO
{
    [SerializeField] private GameObject rangeWeaponPrefab;
    [SerializeField] private float speed = 1f;
    [SerializeField] private float attackRange = 5f;
    [SerializeField] private float rotationSpeed = 1f;
    [SerializeField] private float radius = 1f;

    public GameObject RangeWeaponPrefab => rangeWeaponPrefab;
    public float AttackRange => attackRange;
    public float Speed => speed;

    public override void DrawWeaponGizmos(Vector3 origin, Vector3 direction)
    {
        Gizmos.DrawLine(origin, origin + direction * attackRange);
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

    public override GameObject PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
        if (!TryAttack()) return null;

        GameObject rangeWeapon = Instantiate(rangeWeaponPrefab, origin.position, Quaternion.identity);

        if (rangeWeapon.TryGetComponent(out ThrowableWeapon throwable))
        {
            throwable.Initialize(this, direction, rotationSpeed, radius, hittableMask);
        }

        return rangeWeaponPrefab;
    }

    protected override bool TryAttack()
    {
        return true;
    }
}
