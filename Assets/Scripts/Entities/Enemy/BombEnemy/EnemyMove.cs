using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Coroutine retryGetNavMesh;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
    }

    public void Movement(Vector3 targetPosition)
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(targetPosition);
    }

    public void StopMovement()
    {
        navMeshAgent.isStopped = true;
    }


}
