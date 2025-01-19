using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AEntity : MonoBehaviour, IHurt
{
    public float hp;
    public float maxHp;
    public int countCoints;
    public int countKeys;

    public abstract void Hurt(float damage);
}
