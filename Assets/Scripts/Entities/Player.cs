using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AEntity
{
    public static Player player;
    public static AWeaponSO currentWeapon;
    public static Inventory inventory;

    private void Start()
    {
        if (player == null)
        {
            player = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
