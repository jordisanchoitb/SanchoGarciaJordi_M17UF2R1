using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectedWeapons;
    void Start()
    {
        DesactiveAllSelectWeapons();
        foreach (var item in selectedWeapons)
        {
            if (item.name.Contains(Player.currentWeapon.Prefab.name))
            {
                item.SetActive(true);
            }            
        }
    }

    public void EquipSword()
    {
        FindAnyObjectByType<WeaponManager>().EquipWeapon(FindAnyObjectByType<Inventory>().weapons[0]);
        DesactiveAllSelectWeapons();
        selectedWeapons[0].SetActive(true);
    }

    public void EquipRifle()
    {
        FindAnyObjectByType<WeaponManager>().EquipWeapon(FindAnyObjectByType<Inventory>().weapons[1]);
        DesactiveAllSelectWeapons();
        selectedWeapons[1].SetActive(true);
    }

    public void EquipGrenadeLauncher()
    {
        FindAnyObjectByType<WeaponManager>().EquipWeapon(FindAnyObjectByType<Inventory>().weapons[2]);
        DesactiveAllSelectWeapons();
        selectedWeapons[2].SetActive(true);
    }

    public void EquipFlameThrower()
    {
        FindAnyObjectByType<WeaponManager>().EquipWeapon(FindAnyObjectByType<Inventory>().weapons[3]);
        DesactiveAllSelectWeapons();
        selectedWeapons[3].SetActive(true);
    }

    public void DesactiveAllSelectWeapons()
    {
        foreach (GameObject weapon in selectedWeapons)
        {
            weapon.SetActive(false);
        }
    }

}
