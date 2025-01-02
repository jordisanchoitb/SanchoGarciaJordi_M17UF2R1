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
        if (other.TryGetComponent<IHurt>(out var enemy))
        {
            enemy.Hurt(damage);
        }
    }
}
