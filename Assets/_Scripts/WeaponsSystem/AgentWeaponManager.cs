using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Pool;

public class AgentWeaponManager : MonoBehaviour
{
    [SerializeField] private BaseWeaponDataSO initialWeapon;
    [SerializeField] private Transform weaponPosition;
    private Vector2 direction;

    private SpriteRenderer weaponSprite;
    private WeaponStorage weaponStorage;

    public UnityEvent<Sprite> OnWeaponSwap;
    public UnityEvent OnMultipleWeapons;
    public UnityEvent OnWeaponPickup;

    private bool isAttackingIdle;
    private bool isAttackingMoving;
    private bool isAttackingJumping;
    private bool isAttackingClimbing;

    [SerializeField] private Color weaponRangeColor = Color.blue;
    public Transform WeaponPosition => weaponPosition;
    public bool IsAttacking => isAttackingIdle || isAttackingMoving || isAttackingJumping || isAttackingClimbing;
    public bool IsAttackingIdle => isAttackingIdle;
    public bool IsAttackingMoving => isAttackingMoving;
    public bool IsAttackingJumping => isAttackingJumping;
    public bool IsAttackingClimbing => isAttackingClimbing;
    

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

    public BaseWeaponDataSO GetSecondaryWeapon()
    {
        return weaponStorage.GetCurrentWeapon();
    }

    private void SwapWeaponSprite(Sprite weaponSprite)
    {
        this.weaponSprite.sprite = weaponSprite;
        OnWeaponSwap?.Invoke(weaponSprite);
    }

    public void ResetAttack()
    {
        isAttackingIdle = false;
        isAttackingMoving = false;
        isAttackingJumping = false;
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
    }

    public List<string> GetPlayerWeaponNames()
    {
        return weaponStorage.GetPlayerWeaponNames();
    }

    public void TryPerformAttack(bool isGrounded, bool isClimbing = false)
    {
        if (GetCurrentWeapon().TryAttack(isGrounded, isClimbing))
        {
            if (GetCurrentWeapon().CanUseWhileMoving)
            {
                isAttackingMoving = true;
            }
            else
            {
                isAttackingIdle = true;
            }
            if (GetCurrentWeapon().CanUseWhileJumping)
            {
                isAttackingJumping = true;
            }

            if (GetCurrentWeapon().CanUseWhileClimbing)
            {
                isAttackingClimbing = true;
            }
        }
    }

    public void PerformAttack(LayerMask hittableTarget)
    {
        direction = transform.parent.parent.transform.right * (transform.parent.parent.transform.localScale.x > 0 ? 1 : -1);

        GetCurrentWeapon().PerformAttack(WeaponPosition, hittableTarget, direction);
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying == false) return;

        if (GetCurrentWeapon() == null) return;

        Gizmos.color = weaponRangeColor;

        direction = transform.parent.parent.transform.right * (transform.parent.parent.transform.localScale.x > 0 ? 1 : -1);

        GetCurrentWeapon().DrawWeaponGizmos(weaponPosition.position, direction);
    }
}
