using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "RifleSO", menuName = "ScriptableObjects/Weapons/RifleSO")]

public class RifleSO : AWeaponSO
{
    private const float NINETEEN = 90f;
    private float nextFireTime;
    public override void Shoot()
    {
        if (Time.time >= nextFireTime)
        {
            // Obtener una bala del pool
            GameObject bullet = Weapon.GetComponentInChildren<BulletPool>().GetBullet();

            // Configurar posición y rotación inicial de la bala
            bullet.transform.position = Weapon.GetComponentInChildren<Transform>().position;

            // Pillar la rotacion a la que esta mirando la arma en el momento de disparar
            Quaternion rotation = Quaternion.Euler(0, 0, Weapon.GetComponent<WeaponRotationPlayer>().GetCurrentAngle() - NINETEEN);
            bullet.transform.rotation = rotation;

            // Configurar la dirección inicial de la bala
            bullet.GetComponent<Bullet>().SetDirection(Weapon.GetComponentInChildren<Transform>().right);

            nextFireTime = Time.time + 1f / fireRate;

            AudioManager.audioManager.PlaySoundEffectShotRifle();
        }
    }

    public override void Stop()
    {
        
    }
}
