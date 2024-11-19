using System;
using UnityEngine;

public abstract class AWeaponSO : ScriptableObject
{
    protected int Damage { get; set; }
    public abstract void Shoot();
}