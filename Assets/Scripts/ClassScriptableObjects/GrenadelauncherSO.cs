using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "GrenadelauncherSO", menuName = "ScriptableObjects/Weapons/GrenadelauncherSO")]

public class GrenadelauncherSO : AWeaponSO
{
    private const float NINETEEN = 90f;
    [SerializeField] private float nextFireTime = 0f;
    [SerializeField] private float distanceLaunch;
    private void OnEnable()
    {
        nextFireTime = 0f;
    }
    public override void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            // Obtener una granada del pool
            GameObject grenadeObject = Weapon.GetComponentInChildren<GrenadePool>().GetGrenade();

            // Configurar posición y rotación de la granada
            grenadeObject.transform.position = Weapon.GetComponentInChildren<Transform>().position;

            Quaternion rotation = Quaternion.Euler(0, 0, Weapon.GetComponent<WeaponRotationPlayer>().GetCurrentAngle() - NINETEEN);
            grenadeObject.transform.rotation = rotation;

            // Lanzar la granada
            grenadeObject.GetComponent<Grenade>().Launch(Weapon.GetComponentInChildren<Transform>().right * distanceLaunch);

            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    public override void Stop()
    {
        
    }
}
