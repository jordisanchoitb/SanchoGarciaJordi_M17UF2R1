using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;

public class TableShopRifle : MonoBehaviour
{
    private Collider2D collider2D;
    [SerializeField] private TextMeshProUGUI textCost;
    [SerializeField] private GameObject imageRifle;
    void Start()
    {
        collider2D = GetComponent<Collider2D>();
        foreach (AWeaponSO weapon in gameObject.GetComponentInParent<ShopManager>().purchableWeapons)
        {
            if (weapon is RifleSO && !Player.inventory.weaponsGetted.Contains(weapon.Prefab.name))
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
            if (Player.player.countCoints >= Convert.ToInt32(textCost.text) && !Player.inventory.weaponsGetted.Contains("Rifle"))
            {
                Player.player.countCoints -= Convert.ToInt32(textCost.text);
                Player.inventory.weaponsGetted.Add("Rifle");
                Player.player.UpdateTextCoints();
                collider2D.enabled = false;
                imageRifle.SetActive(false);
            }
        }
        
    }
}
