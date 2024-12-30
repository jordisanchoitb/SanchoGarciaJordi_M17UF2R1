using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : AEntity
{
    public static Player player;
    public static AWeaponSO currentWeapon;
    public static Inventory inventory;
    private Slider healthBar;

    private void Start()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
            healthBar = GameObject.Find("PlayerHp").GetComponent<Slider>();
            healthBar.maxValue = hp;
            healthBar.value = hp;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public override void Hurt(int damage)
    {
        hp -= damage;
        healthBar.value = hp;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
