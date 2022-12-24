using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponStorage
{
    private readonly List<BaseWeaponDataSO> weaponDataList = new();
    private int currentWeaponIndex = -1;

    public int WeaponCount => weaponDataList.Count;

    internal BaseWeaponDataSO GetCurrentWeapon()
    {
        if (currentWeaponIndex < 0)
        {
            return null;
        }

        return weaponDataList[currentWeaponIndex];
    }

    internal BaseWeaponDataSO SwapWeapon()
    {
        if (currentWeaponIndex < 0)
        {
            return null;
        }

        currentWeaponIndex++;
        if (currentWeaponIndex >= WeaponCount)
        {
            currentWeaponIndex = 0;
        }

        return weaponDataList[currentWeaponIndex];
    }

    internal bool AddWeaponData(BaseWeaponDataSO weaponData)
    {
        if (weaponDataList.Contains(weaponData))
        {
            return false;
        }
        weaponDataList.Add(weaponData);
        currentWeaponIndex = WeaponCount - 1;
        return true;
    }

    internal List<string> GetPlayerWeaponNames()
    {
        return weaponDataList.Select(weapon=> weapon.name).ToList();
    }
}
