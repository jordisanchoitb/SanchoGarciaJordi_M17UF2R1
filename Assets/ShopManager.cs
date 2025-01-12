using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textCostRifle, textCostGrenadeLauncher, textCostFlameThrower;
    [SerializeField] private GameObject imageRifle, imageGrenadeLauncher, imageFlameThrower;
    void Start()
    {
        foreach (AWeaponSO weapon in Player.inventory.weapons)
        {
            if (!Player.inventory.obtainedWeapons.Contains(weapon.Prefab.name))
            {

                switch (weapon.Prefab.name)
                {
                    case "Rifle":
                        textCostRifle.text = weapon.Cost.ToString();
                        break;
                    case "GrenadeLauncher":
                        textCostGrenadeLauncher.text = weapon.Cost.ToString();
                        break;
                    case "FlameThrower":
                        textCostFlameThrower.text = weapon.Cost.ToString();
                        break;
                }
            }
            else
            {
                switch (weapon.Prefab.name)
                {
                    case "Rifle":
                        imageRifle.SetActive(false);
                        break;
                    case "GrenadeLauncher":
                        imageGrenadeLauncher.SetActive(false);
                        break;
                    case "FlameThrower":
                        imageFlameThrower.SetActive(false);
                        break;
                }
            }
        }
    }
}
