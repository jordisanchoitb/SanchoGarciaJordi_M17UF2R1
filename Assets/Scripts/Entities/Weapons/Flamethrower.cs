using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
    public int damage;
    void Start()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("Collided with " + other.name);
        if (other.TryGetComponent<IHurt>(out var enemy))
        {
            enemy.Hurt(damage);
        }
    }
}
