using System;
using UnityEngine;
using System.Collections.Generic;

public abstract class AStateSO<T> : ScriptableObject
{
    public List<AStateSO<T>> StatesToGo = new List<AStateSO<T>>();
    public abstract void OnStateEnter(T entityController);
    public abstract void OnStateUpdate(T entityController);
    public abstract void OnStateExit(T entityController);
}