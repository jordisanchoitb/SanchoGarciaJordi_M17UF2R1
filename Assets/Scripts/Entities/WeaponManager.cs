using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour, PlayerControlers.IWeaponActions
{
    private const int NINETEEN = 90;
    private PlayerControlers pControlers;
    private Animator animator;
    private BulletPool bulletPool;
    private WeaponRotationPlayer wrPlayer;
    private GrenadePool grenadePool;
    [SerializeField]
    private Transform firePoint;
    [SerializeField]
    private Transform firePointGrenadeLauncher;
    private float fireRate = 5f;
    private float nextFireTime;
    private float launchForce = 10f;
    private ParticleSystem particleSystem;
    
    private void Awake()
    {
        pControlers = new PlayerControlers();
        wrPlayer = gameObject.GetComponent<WeaponRotationPlayer>();
        pControlers.Weapon.SetCallbacks(this);
        if (TryGetComponent(out Animator anim))
        {
            animator = anim;
        }

        try 
        {
            bulletPool = gameObject.GetComponentInChildren<BulletPool>();
        } catch
        {
            Debug.LogError("No Bullet Pool Found");
        }

        try
        {
            grenadePool = gameObject.GetComponentInChildren<GrenadePool>();
        } catch
        {
            Debug.LogError("No Grenade Pool Found");
        }

        try
        {
            particleSystem = gameObject.GetComponentInChildren<ParticleSystem>();
        }
        catch
        {
            Debug.LogError("No Particle System Found");
        }
    }  

    private void OnEnable()
    {
        pControlers.Enable();
        if (animator != null)
        {
            animator.SetBool("Atk", false);
        }
    }

    private void OnDisable()
    {
        pControlers.Disable();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (!context.canceled || context.performed)
        {
            switch (gameObject.name)
            {
                case "Sword":
                    Debug.Log("Sword Damage");
                    animator.SetBool("Atk", true);
                    break;
                case "Flamethrower":
                    Debug.Log("Flamethrower Damage");
                    particleSystem.Play();
                    break;
                case "Grenadelauncher":
                    Debug.Log("Grenadelauncher Damage");
                    if (Time.time >= nextFireTime)
                    {
                        // Obtener una granada del pool
                        GameObject grenadeObject = grenadePool.GetGrenade();

                        // Configurar posición y rotación de la granada
                        grenadeObject.transform.SetPositionAndRotation(firePointGrenadeLauncher.position, firePointGrenadeLauncher.rotation);

                        // Lanzar la granada
                        Grenade grenade = grenadeObject.GetComponent<Grenade>();
                        grenade.Launch(firePoint.right, launchForce);

                        nextFireTime = Time.time + 1f / fireRate;
                    }
                    break;
                case "Rifle":
                    Debug.Log("Rifle Damage");
                    if (Time.time >= nextFireTime)
                    {
                        // Obtener una bala del pool
                        GameObject bullet = bulletPool.GetBullet();

                        // Configurar posición y rotación inicial de la bala
                        bullet.transform.position = firePoint.position;

                        // Pillar la rotacion a la que esta mirando la arma en el momento de disparar
                        Quaternion rotation = Quaternion.Euler(0, 0, wrPlayer.GetCurrentAngle() - NINETEEN);
                        bullet.transform.rotation = rotation;

                        // Configurar la dirección inicial de la bala
                        bullet.GetComponent<Bullet>().SetDirection(firePoint.right);

                        nextFireTime = Time.time + 1f / fireRate;
                    }
                    break;
                default:
                    Debug.Log("No Weapon");
                    break;
            }
        }
        
        if (context.canceled && CompareTag("Sword"))
        {
            animator.SetBool("Atk", false);
        }

        if (context.canceled && CompareTag("Flamethrower"))
        {
            Debug.Log("Flamethrower Stop");
            particleSystem.Stop();
        }
    }

    public bool IsFiring()
    {
        return particleSystem.isPlaying;
    }
}
