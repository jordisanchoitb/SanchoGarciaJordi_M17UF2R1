using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public float damage = 1;
    public float pushForce = 1.1f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<IHurt>(out var enemy))
        {
            enemy.Hurt(damage);
            ApplyPush(transform.position, collision.GetComponent<Rigidbody2D>());
        }
    }

    public void ApplyPush(Vector2 origin, Rigidbody2D target)
    {
        if (target == null) return;

        // Calcular la dirección del empuje desde el origen hacia el objeto
        Vector2 pushDirection = (target.position - origin).normalized;

        // Aplicar la fuerza al Rigidbody2D del objeto
        target.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
    }
}