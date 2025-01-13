using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<AWeaponSO> weapons = new List<AWeaponSO>();

    private void Start()
    {
    }

    public void AddWeapon(AWeaponSO weapon)
    {
        weapons.Add(weapon);
    }

    public void ResetInventoryDefault()
    {
        weapons.Clear();

    }


}
