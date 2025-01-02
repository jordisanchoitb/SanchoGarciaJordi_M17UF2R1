using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Player : AEntity, ICollect
{
    public static Player player;
    public static AWeaponSO currentWeapon;
    public static Inventory inventory;
    private Slider healthBar;
    private TextMeshProUGUI textCountCoins;
    private TextMeshProUGUI textCountKeys;

    private void Start()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
            healthBar = GameObject.Find("PlayerHp").GetComponent<Slider>();
            textCountCoins = GameObject.Find("TextCountCoins").GetComponent<TextMeshProUGUI>();
            textCountKeys = GameObject.Find("TextCountKeys").GetComponent<TextMeshProUGUI>();
            healthBar.maxValue = hp;
            healthBar.value = hp;

            if (GameEventsManager.gameEventsManager != null)
            {
                GameEventsManager.gameEventsManager.OnDoorInteracted += CheckKeysForDoor;
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDisable()
    {
        if (GameEventsManager.gameEventsManager != null)
        {
            GameEventsManager.gameEventsManager.OnDoorInteracted -= CheckKeysForDoor;
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

    public void Collect(GameObject collectable)
    {
        if (collectable.gameObject.name.Contains("Coin"))
        {
            countCoints++;
            textCountCoins.text = countCoints.ToString();
        }
        else if (collectable.gameObject.name.Contains("Key"))
        {
            countKeys++;
            textCountKeys.text = countKeys.ToString();
        }
    }

    private void CheckKeysForDoor(int keysRequiered, Action onSuccess)
    {
        if (countKeys >= keysRequiered)
        {
            countKeys -= keysRequiered;
            textCountKeys.text = countKeys.ToString();
            onSuccess?.Invoke();
        }
    }
}
