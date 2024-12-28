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
    private int MaxHp;

    private void OnEnable()
    {
        hp = MaxHp;
        healthBar.value = hp;
        countCoints = 0;
        countKeys = 0;
    }


    private void Start()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
            healthBar = GameObject.Find("PlayerHp").GetComponent<Slider>();
            healthBar.maxValue = hp;
            healthBar.value = hp;
            MaxHp = hp;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Hit(1);
        }
    }

    public void Hit(float damage)
    {
        hp -= (int)damage;
        healthBar.value = hp;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
