using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data/Null", fileName = "WeaponData_Null")]
public class NullWeaponDataSO : BaseWeaponDataSO
{
    protected override bool TryAttack()
    {
        return false;
    }

    public override bool CheckIfTargetInRange(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
        return false;
    }
}
