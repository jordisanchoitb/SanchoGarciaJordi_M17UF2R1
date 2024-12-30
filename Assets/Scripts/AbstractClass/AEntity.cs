using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public abstract class AEntity : MonoBehaviour, IHurt
{
    public int hp;
    public int countCoints;
    public int countKeys;

    public abstract void Hurt(int damage);
}
