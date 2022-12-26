using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AgentWeaponManager : MonoBehaviour
{
    [SerializeField] private BaseWeaponDataSO initialWeapon;

    private SpriteRenderer weaponSprite;

    private WeaponStorage weaponStorage;

    public UnityEvent<Sprite> OnWeaponSwap;
    public UnityEvent OnMultipleWeapons;
    public UnityEvent OnWeaponPickup;

    [SerializeField] private Color weaponRangeColor = Color.blue;

    private void Awake()
    {
        weaponStorage = new WeaponStorage();
        weaponSprite = GetComponent<SpriteRenderer>();
        AddWeaponData(initialWeapon);
        ToggleWeaponVisibility(false);
    }

    public void ToggleWeaponVisibility(bool val)
    {
        if (val)
        {
            SwapWeaponSprite(GetCurrentWeapon().WeaponSprite);
        }

        weaponSprite.enabled = val;
    }

    public BaseWeaponDataSO GetCurrentWeapon()
    {
        return weaponStorage.GetCurrentWeapon();
    }

    private void SwapWeaponSprite(Sprite weaponSprite)
    {
        this.weaponSprite.sprite = weaponSprite;
        OnWeaponSwap?.Invoke(weaponSprite);
    }

    public void SwapWeapon()
    {
        if (weaponStorage.WeaponCount <= 0)
        {
            return;
        }

        SwapWeaponSprite(weaponStorage.SwapWeapon().WeaponSprite);
    }

    public void AddWeaponData(BaseWeaponDataSO weaponData)
    {
        if (!weaponStorage.AddWeaponData(weaponData))
        {
            return;
        }

        if (weaponStorage.WeaponCount >= 2)
        {
            OnMultipleWeapons?.Invoke();
        }

        SwapWeaponSprite(weaponData.WeaponSprite);
    }

    public void PickupWeapon(BaseWeaponDataSO weaponData)
    {
        AddWeaponData(weaponData);
        OnWeaponPickup?.Invoke();
    }

    public bool CanIUseWeapon()
    {
        if (weaponStorage.WeaponCount <= 0)
        {
            return false;
        }

        return true;

        // return weaponStorage.GetCurrentWeapon().CanBeUsed(isGrounded);
    }

    public List<string> GetPlayerWeaponNames()
    {
        return weaponStorage.GetPlayerWeaponNames();
    }
}
