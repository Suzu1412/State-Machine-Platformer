using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Weapon Data/Null", fileName = "WeaponData_Null")]
public class NullWeaponDataSO : BaseWeaponDataSO
{
    public override bool TryAttack(bool isGrounded, bool isClimbing)
    {
        return false;
    }

    public override bool CheckIfTargetInRange(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
        return false;
    }
}
