using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SwordSO", menuName = "ScriptableObjects/Weapons/SwordSO")]

public class SwordSO : AWeaponSO
{
    public override void Shoot()
    {
        Weapon.GetComponent<Animator>().SetTrigger("Atk");
    }

    public override void Stop()
    {
        
    }
}
