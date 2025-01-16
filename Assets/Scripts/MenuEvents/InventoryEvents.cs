using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryEvents : MonoBehaviour
{
    [SerializeField] private List<GameObject> selectedWeapons;
    [SerializeField] private List<GameObject> bloquedWeapons;
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


        foreach (AWeaponSO weapon in FindAnyObjectByType<ShopManager>().purchableWeapons)
        {
            switch (weapon)
            {
                case RifleSO rifle:
                    bloquedWeapons[0].SetActive(true);
                    break;
                case GrenadelauncherSO grenade:
                    bloquedWeapons[1].SetActive(true);
                    break;
                case FlameThrowerSO flame:
                    bloquedWeapons[2].SetActive(true);
                    break;

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
        if (IsWeaponBloqued(FindAnyObjectByType<Inventory>().weapons[1])) return;
        FindAnyObjectByType<WeaponManager>().EquipWeapon(FindAnyObjectByType<Inventory>().weapons[1]);
        DesactiveAllSelectWeapons();
        selectedWeapons[1].SetActive(true);
    }

    public void EquipGrenadeLauncher()
    {
        if (IsWeaponBloqued(FindAnyObjectByType<Inventory>().weapons[2])) return;
        FindAnyObjectByType<WeaponManager>().EquipWeapon(FindAnyObjectByType<Inventory>().weapons[2]);
        DesactiveAllSelectWeapons();
        selectedWeapons[2].SetActive(true);
    }

    public void EquipFlameThrower()
    {
        if (IsWeaponBloqued(FindAnyObjectByType<Inventory>().weapons[3])) return;
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

    public bool IsWeaponBloqued(AWeaponSO weapon)
    {
        foreach (AWeaponSO item in FindAnyObjectByType<ShopManager>().purchableWeapons)
        {
            if (item.Prefab.name == weapon.Prefab.name)
            {
                return true;
            }
        }
        return false;
    }

}
