using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAttack
{
    void PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction);

    void DrawWeaponGizmos(Vector3 origin, Vector3 direction);
}
