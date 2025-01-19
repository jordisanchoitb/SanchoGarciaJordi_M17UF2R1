using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCostRifle, textCostGrenadeLauncher, textCostFlameThrower;
    [SerializeField] private GameObject imageRifle, imageGrenadeLauncher, imageFlameThrower;
    [SerializeField] public List<AWeaponSO> purchableWeapons;
    void Start()
    {
        foreach (var weapon in purchableWeapons)
        {
            if (weapon is RifleSO && !Player.inventory.weaponsGetted.Contains(weapon.Prefab.name))
            {
                textCostRifle.text = weapon.Cost.ToString();
            }
            else if (weapon is GrenadelauncherSO && !Player.inventory.weaponsGetted.Contains(weapon.Prefab.name))
            {
                textCostGrenadeLauncher.text = weapon.Cost.ToString();
            }
            else if (weapon is FlameThrowerSO && !Player.inventory.weaponsGetted.Contains(weapon.Prefab.name))
            {
                textCostFlameThrower.text = weapon.Cost.ToString();
            }

            if (Player.inventory.weaponsGetted.Contains(weapon.Prefab.name))
            {
                if (weapon is RifleSO)
                {
                    imageRifle.SetActive(false);
                }
                else if (weapon is GrenadelauncherSO)
                {
                    imageGrenadeLauncher.SetActive(false);
                }
                else if (weapon is FlameThrowerSO)
                {
                    imageFlameThrower.SetActive(false);
                }
            }
        }
    }
}
