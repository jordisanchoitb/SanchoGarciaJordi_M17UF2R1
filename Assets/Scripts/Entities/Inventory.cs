using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<AWeaponSO> weapons = new List<AWeaponSO>();
    public List<string> weaponsGetted = new List<string>();

    private void Start()
    {
        weaponsGetted.Add("Sword");
    }

    public void ResetGettedWeapons()
    {
        weaponsGetted.Clear();
        weaponsGetted.Add("Sword");
    }


}
