using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : AEntity
{
    public static Inventory inventory;
    public static List<AWeaponSO> weapons = new List<AWeaponSO>();

    public void AddWeapon(AWeaponSO weapon)
    {
        weapons.Add(weapon);
    }

    public void ResetInventoryDefault()
    {
        weapons.Clear();

    }

}
