using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Grenade Properties")]
    [SerializeField]
    private int damage;
    [SerializeField]
    private float explosionRadius;
    [SerializeField]
    private float explosionDelay;
    private float tmpexplosionDelay;
    [SerializeField]
    private float speed;
    [SerializeField]
    private LayerMask damageableLayer;
    private Rigidbody2D rigidBody2D;


    private void OnEnable()
    {
        tmpexplosionDelay = explosionDelay;
    }

    private void OnDisable()
    {
        try
        {
            rigidBody2D.velocity = Vector2.zero;
            explosionDelay = tmpexplosionDelay;
            GameObject.FindGameObjectWithTag("Grenadelauncher").GetComponentInChildren<GrenadePool>().ReturnGrenade(gameObject);
        } catch (NullReferenceException)
        {
            Destroy(gameObject);
        }
    }

    private void Awake()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector3 targetposition)
    {
        Vector3 moveDirection = targetposition.normalized;
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
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);

        foreach (Collider2D obj in objects)
        {
            if (obj.gameObject.TryGetComponent<IHurt>(out var enemy) && obj.gameObject.name.Contains("Enemy"))
            {
                Vector2 closestPoint = obj.ClosestPoint(transform.position);
                float distance = Vector2.Distance(closestPoint, transform.position);

                int damagePercent = (int)Mathf.InverseLerp(explosionRadius, 0, distance);

                enemy.Hurt(damagePercent * damage);
            }
        }

        ResetGrenade();
    }

    private void ResetGrenade()
    {
        rigidBody2D.velocity = Vector2.zero;
        gameObject.SetActive(false);
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
