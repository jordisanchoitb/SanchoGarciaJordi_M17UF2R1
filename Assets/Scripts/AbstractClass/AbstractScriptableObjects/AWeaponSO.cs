using System;
using UnityEngine;


public abstract class AWeaponSO : ScriptableObject, IShoot
{
    [SerializeField] protected int damage;
    [SerializeField] protected float fireRate;
    [SerializeField] protected int cost;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected GameObject weapon;

    public int Damage { get => damage; }
    public float FireRate { get => fireRate; }
    public int Cost { get => cost; }
    public GameObject Prefab { get => prefab; }
    public GameObject Weapon { get => weapon; set => weapon = value; }
    public abstract void Shoot();
    public abstract void Stop();
}