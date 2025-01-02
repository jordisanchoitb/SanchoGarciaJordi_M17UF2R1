using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[CreateAssetMenu(fileName = "ShotTurretState", menuName = "ScriptableObjects/Enemy/TurretEnemy/ShotTurretState")]

public class ShotTurretState : AStateSO<EnemyTurretFSM>
{
    private Transform player;
    private BulletPool bulletPool;
    private Transform firePoint; 
    private SpriteRenderer spriteRenderer;
    private float fireRate = 1f;
    private float fireCooldown;
    [SerializeField] private LayerMask obstacleMask;
    public override void OnStateEnter(EnemyTurretFSM entityController)
    {
        player = GameObject.Find("Player").transform;
        bulletPool = GameObject.Find("GameManager").GetComponent<BulletPool>();
        spriteRenderer = entityController.GetComponent<SpriteRenderer>();
        firePoint = entityController.GetComponentInChildren<Transform>();
    }
    
    public override void OnStateExit(EnemyTurretFSM entityController)
    {
    }

    public override void OnStateUpdate(EnemyTurretFSM entityController)
    {
        fireCooldown -= Time.deltaTime;

        if (IsPlayerVisible(entityController))
        {
            // Disparar si est� en cooldown
            if (fireCooldown <= 0f)
            {
                Shoot();
                fireCooldown = fireRate;
            }
            if (player.transform.position.x > entityController.transform.position.x)
            {
                // El jugador est� a la derecha
                spriteRenderer.flipX = false;
            }
            else
            {
                // El jugador est� a la izquierda
                spriteRenderer.flipX = true;
            }
        }
    }

    private bool IsPlayerVisible(EnemyTurretFSM entityController)
    {
        Vector2 direction = (player.position - entityController.transform.position).normalized;
        float distanceToPlayer = Vector2.Distance(entityController.transform.position, player.position);

        // Comprobar si hay un obst�culo entre la torreta y el jugador
        RaycastHit2D hit = Physics2D.Raycast(entityController.transform.position, direction, distanceToPlayer, obstacleMask);
        return hit.collider == null;
    }

    private void Shoot()
    {
        if (bulletPool == null)
        {
            Debug.LogError("BulletPool no asignado a la torreta.");
            return;
        }

        // Obt�n una bala del pool
        GameObject bullet = bulletPool.GetBullet();

        if (bullet != null)
        {
            // Posiciona y rota la bala en el punto de disparo
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = Quaternion.LookRotation(Vector3.forward, player.position - firePoint.position) * Quaternion.Euler(0, 0, 90);

            // Activa la bala
            bullet.SetActive(true);

            // Configura la direcci�n de la bala hacia el jugador
            bullet.GetComponent<EnemyBullet>().SetDirection((player.position - firePoint.position).normalized);
        }

    }
}

