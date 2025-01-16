using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : AEntity, ICollect
{
    public static Player player;
    public static AWeaponSO currentWeapon;
    public static Inventory inventory;
    [SerializeField] private Slider healthBar;
    [SerializeField] private TextMeshProUGUI textCountCoins;
    [SerializeField] private TextMeshProUGUI textCountKeys;
    public static bool IsPaused;

    private void OnEnable()
    {
        if (player != null && Player.player == this)
        {
            player.transform.position = new Vector3(0, 0, 0);
            hp = maxHp;
            healthBar.value = hp;
            countCoints = 0;
            textCountCoins.text = countCoints.ToString();
            countKeys = 0;
            textCountKeys.text = countKeys.ToString();
            if (GameEventsManager.gameEventsManager != null)
            {
                GameEventsManager.gameEventsManager.OnDoorInteracted += CheckKeysForDoor;
            }
        }
    }

    private void Start()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
            healthBar.maxValue = hp;
            healthBar.value = hp;
            maxHp = hp;
            IsPaused = false;

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

    public override void Hurt(float damage)
    {
        hp -= damage;
        healthBar.value = hp;

        if (hp <= 0)
        {
            gameObject.SetActive(false);
            SceneManager.LoadScene("DieMenu", LoadSceneMode.Additive);
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
