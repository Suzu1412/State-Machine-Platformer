using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponDataSO : ScriptableObject, IAttack
{
    [SerializeField] protected string weaponName;
    [SerializeField] protected Sprite weaponSprite;
    [SerializeField] protected int weaponDamage = 1;
    [SerializeField] protected AudioClip weaponSwingSound;
    protected Color attackRangeColor = Color.blue;
    protected Color attackHitColor = Color.white;

    public string WeaponName => weaponName;
    public Sprite WeaponSprite => weaponSprite;
    public int WeaponDamage => weaponDamage;
    public AudioClip WeaponSwingSound => weaponSwingSound;

    public bool Equals(WeaponDataSO other)
    {
        return weaponName == other.WeaponName;
    }

    protected abstract bool TryAttack();

    public virtual void DrawWeaponGizmos(Vector3 origin, Vector3 direction)
    {
    }

    public virtual void PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
    }


}
