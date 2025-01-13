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
            if (weapon is RifleSO)
            {
                textCostRifle.text = weapon.Cost.ToString();
            }
            else if (weapon is GrenadelauncherSO)
            {
                textCostGrenadeLauncher.text = weapon.Cost.ToString();
            }
            else if (weapon is FlameThrowerSO)
            {
                textCostFlameThrower.text = weapon.Cost.ToString();
            }
        }
    }
}
