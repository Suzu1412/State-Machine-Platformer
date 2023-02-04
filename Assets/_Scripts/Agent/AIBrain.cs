using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBrain : MonoBehaviour
{
    [SerializeField] private AIBrainDataSO data;

    public BaseWeaponDataSO SelectCurrentWeapon()
    {
        return data.WeaponList[Random.Range(0, data.WeaponList.Length)] ;
    }

}
