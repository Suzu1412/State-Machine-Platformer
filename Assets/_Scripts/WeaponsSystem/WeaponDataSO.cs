using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WeaponDataSO : ScriptableObject, IEquatable<WeaponDataSO>
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

    public abstract bool CanBeUsed(bool isGrounded);

    public abstract void PerformAttack(Transform origin, LayerMask hittableMask, Vector3 direction);

    public virtual void DrawWeaponGizmo(Vector3 origin, Vector3 direction)
    {

    } 
}
