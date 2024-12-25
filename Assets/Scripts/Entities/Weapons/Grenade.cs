using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Grenade Properties")]
    [SerializeField]
    private float damage = 20f;
    [SerializeField]
    private float explosionRadius = 5f;
    [SerializeField]
    private float explosionDelay = 3f;
    private float tmpexplosionDelay;
    [SerializeField]
    private float speed = 250f;
    private Rigidbody2D rigidBody2D;
    private bool isExploded = false;

    private void OnEnable()
    {
        isExploded = false;
        tmpexplosionDelay = explosionDelay;
    }

    private void OnDisable()
    {
        rigidBody2D.velocity = Vector2.zero;
        isExploded = false;
        explosionDelay = tmpexplosionDelay;
        GameObject.FindGameObjectWithTag("Grenadelauncher").GetComponentInChildren<GrenadePool>().ReturnGrenade(gameObject);
    }

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector3 targetposition)
    {
        Vector3 moveDirection = (targetposition - transform.position).normalized;
        rigidBody2D.velocity = moveDirection * speed;
    }

    private void Update()
    {
        explosionDelay -= Time.deltaTime;
        if (explosionDelay <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if (isExploded)
        {
            return;
        }
        isExploded = true;
        // Efecto de explosión / partícula 
        Debug.Log("Granada explota!");

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D obj in objects)
        {
            var enemy = obj.GetComponent<EnemyBombFSM>();
            if (enemy != null)
            {
                Debug.Log("Enemy hit!");
                enemy.Hit(damage);
            }
        }

        ResetGrenade();
    }

    private void ResetGrenade()
    {
        rigidBody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
