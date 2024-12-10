using System;
using UnityEngine;


public abstract class AWeaponSO : ScriptableObject, IShoot
{
    protected int Damage { get; set; }
    public abstract void Shoot();
}