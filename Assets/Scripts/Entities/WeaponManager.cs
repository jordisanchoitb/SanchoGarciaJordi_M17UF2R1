using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour, PlayerControlers.IWeaponActions
{
    private PlayerControlers pControlers;
    [SerializeField] private AWeaponSO weaponSO;

    private void Awake()
    {
        pControlers = new PlayerControlers();
        pControlers.Weapon.SetCallbacks(this);
    }

    private void OnEnable()
    {
        pControlers.Enable();
        if (weaponSO.Weapon == null)
        {
            weaponSO.Weapon = Instantiate(weaponSO.Prefab, transform.position + new Vector3(0.87f,0,0), Quaternion.identity, transform);
        }
        Player.currentWeapon = weaponSO;
    }

    private void OnDisable()
    {
        pControlers.Disable();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.canceled || context.performed)
        {
            weaponSO.Shoot();
        }

        if (context.canceled && Player.currentWeapon.Weapon.CompareTag("Flamethrower"))
        {
            weaponSO.Stop();
        }
    }
}
