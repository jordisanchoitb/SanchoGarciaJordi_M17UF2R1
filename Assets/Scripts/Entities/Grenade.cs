using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField]
    private float damage = 20f;
    [SerializeField]
    private float explosionRadius = 5f;
    [SerializeField]
    private float explosionDelay = 3f;
    private LayerMask damageableLayer;

    private Rigidbody2D rigidBody2D;


    private void OnDisable()
    {
        GameObject.Find("Grenadelauncher").GetComponentInChildren<GrenadePool>().ReturnGrenade(gameObject);
    }

    private void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction, float force)
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        rigidBody2D.velocity = Vector2.zero; // Asegurar que no haya velocidad previa
        rigidBody2D.AddForce(direction * force, ForceMode2D.Impulse);

        // Iniciar el temporizador de explosión
        Invoke(nameof(Explode), explosionDelay);
    }

    private void OnCollisionEnter2D(Collision2D collision) 
    { 
        Explode();
    }

    private void Explode()
    {
        // Efecto de explosión / partícula 
        Debug.Log("Granada explota!");

        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayer);
        foreach (Collider2D obj in objects)
        {
            var enemy = obj.GetComponent<Enemy>();
            if (enemy != null)
            {
                Debug.Log("Enemy hit!");
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
