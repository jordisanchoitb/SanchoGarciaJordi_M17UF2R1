using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        rigidBody2D.velocity = Vector2.zero;
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
        Debug.Log($"Explosion Radius: {explosionRadius}");

        Collider2D[] objects = Physics2D.OverlapCircleAll(rigidBody2D.position, explosionRadius, damageableLayer);

        Debug.Log($"Objects: {objects.Length}");
        foreach (Collider2D obj in objects)
        {
            if (obj.gameObject.TryGetComponent<IHurt>(out var enemy) && obj.gameObject.name.Contains("Enemy"))
            {
                enemy.Hurt(damage);
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
        Gizmos.DrawWireSphere(rigidBody2D.position, explosionRadius);
    }

}
