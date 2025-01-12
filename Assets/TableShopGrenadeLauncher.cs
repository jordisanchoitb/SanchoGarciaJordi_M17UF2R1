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
        if (!Player.inventory.obtainedWeapons.Contains("GrenadeLauncher"))
        {
            collider2D.enabled = true;
        }
        else
        {
            collider2D.enabled = false;
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
                Player.inventory.obtainedWeapons.Add("GrenadeLauncher");
                collider2D.enabled = false;
                imageGrenadeLauncher.SetActive(false);
            }
        }
        
    }
}
