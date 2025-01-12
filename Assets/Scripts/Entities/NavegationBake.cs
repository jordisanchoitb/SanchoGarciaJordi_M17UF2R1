using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavegationBake : MonoBehaviour
{
    private const float delayTime = 1f;

    private NavMeshSurface navMeshSurface;

    private void Start()
    {
        navMeshSurface = GetComponent<NavMeshSurface>();
        StartCoroutine(DelayedBaking(delayTime));
    }

    private IEnumerator DelayedBaking(float time)
    {
        yield return new WaitForSeconds(time);
        BakeNavMesh();
    }

    private void BakeNavMesh()
    {
        navMeshSurface.BuildNavMesh();
    }
}
