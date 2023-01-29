using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponDataSO : ScriptableObject, IAttack
{
    [SerializeField] protected string weaponName;
    [SerializeField] protected Sprite weaponSprite;
    [SerializeField] protected int weaponDamage = 1;
    [SerializeField] protected AudioClip weaponSwingSound;
    [SerializeField] protected bool canUseWhileMoving;
    [SerializeField] protected bool canUseWhileJumping;
    [SerializeField] protected bool canUseWhileClimbing;

    protected Color attackRangeColor = Color.blue;
    protected Color attackHitColor = Color.white;


    public string WeaponName => weaponName;
    public Sprite WeaponSprite => weaponSprite;
    public int WeaponDamage => weaponDamage;
    public AudioClip WeaponSwingSound => weaponSwingSound;
    public bool CanUseWhileMoving => canUseWhileMoving;
    public bool CanUseWhileJumping => canUseWhileJumping;
    public bool CanUseWhileClimbing => canUseWhileClimbing;

    public bool Equals(WeaponDataSO other)
    {
        return weaponName == other.WeaponName;
    }

    public abstract bool TryAttack(bool isGrounded, bool isClimbing = false);

    public virtual void DrawWeaponGizmos(Vector3 origin, Vector3 direction)
    {
    }

    public virtual GameObject PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
        return null;
    }

    public abstract bool CheckIfTargetInRange(Transform origin, LayerMask hittableMask, Vector3 direction);

}
