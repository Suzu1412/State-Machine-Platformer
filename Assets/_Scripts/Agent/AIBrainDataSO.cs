using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrainDataSO : ScriptableObject
{
    [SerializeField] private int maxAmountOfTimesUseSameAttack = 3;
    [SerializeField] private BaseWeaponDataSO[] weaponList;

    public int MaxAmountOfTimesUseSameAttack => maxAmountOfTimesUseSameAttack;
    public BaseWeaponDataSO[] WeaponList => weaponList;
}
