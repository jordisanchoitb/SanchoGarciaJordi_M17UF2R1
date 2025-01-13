using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class TableShopGrenadeLauncher : MonoBehaviour
{
    private Collider2D collider2D;
    [SerializeField] private TextMeshProUGUI textCost;
    [SerializeField] private GameObject imageGrenadeLauncher;


    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        foreach (AWeaponSO weapon in gameObject.GetComponentInParent<ShopManager>().purchableWeapons)
        {
            if (weapon is GrenadelauncherSO)
            {
                collider2D.enabled = true;
            }
        }
        
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Player.player.countCoints >= Convert.ToInt32(textCost.text))
            {
                Player.player.countCoints -= Convert.ToInt32(textCost.text);
                for (int i = 0; i < gameObject.GetComponentInParent<ShopManager>().purchableWeapons.Count; i++)
                {
                    if (gameObject.GetComponentInParent<ShopManager>().purchableWeapons[i] is GrenadelauncherSO)
                    {
                        gameObject.GetComponentInParent<ShopManager>().purchableWeapons.RemoveAt(i);
                        break;
                    }
                }
                collider2D.enabled = false;
                imageGrenadeLauncher.SetActive(false);
            }
        }
        
    }
}
