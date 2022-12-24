using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeaponDataSO : ScriptableObject, IAttack
{
    [SerializeField] protected string weaponName;
    [SerializeField] protected Sprite weaponSprite;
    [SerializeField] protected int weaponDamage = 1;
    [SerializeField] protected AudioClip weaponSwingSound;

    public string WeaponName => weaponName;
    public Sprite WeaponSprite => weaponSprite;
    public int WeaponDamage => weaponDamage;
    public AudioClip WeaponSwingSound => WeaponSwingSound;

    public bool Equals(WeaponDataSO other)
    {
        return weaponName == other.WeaponName;
    }

    protected abstract bool TryAttack();

    public virtual void DrawWeaponGizmos(Vector3 origin, Vector3 direction, Color color)
    {
    }

    public virtual void PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction)
    {
    }


}
