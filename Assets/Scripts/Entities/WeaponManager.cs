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

    public void EquipWeapon(AWeaponSO newWeapon)
    {
        if (weaponSO.Weapon != null)
        {
            weaponSO.Weapon.SetActive(false); // Desactiva el arma actual
        }

        weaponSO = newWeapon;
        Player.currentWeapon = newWeapon;

        // Si el arma ya existe en la escena, actívala; si no, instánciala
        if (weaponSO.Weapon == null)
        {
            weaponSO.Weapon = Instantiate(weaponSO.Prefab, transform.position, Quaternion.identity, transform);
            weaponSO.Weapon.transform.position += new Vector3(0,0.87f); 
        }
        else
        {
            weaponSO.Weapon.transform.position = transform.position + new Vector3(0,0.87f); // Asegúrate de colocarla correctamente
            weaponSO.Weapon.transform.rotation = Quaternion.identity;
            weaponSO.Weapon.SetActive(true); // Activa el arma
        }
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
