using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FlameThrower", menuName = "ScriptableObjects/Weapons/FlameThrower")]

public class FlameThrowerSO : AWeaponSO
{
    public override void Shoot()
    {
        Weapon.GetComponentInChildren<ParticleSystem>().Play();
    }
    public override void Stop()
    {
        Weapon.GetComponentInChildren<ParticleSystem>().Stop();
    }
}
