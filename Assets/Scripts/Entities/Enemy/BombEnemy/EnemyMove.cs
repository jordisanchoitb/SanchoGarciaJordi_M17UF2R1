using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private bool isNavMeshExceptionNull = false;
    private GameObject Player;
    private bool isEnemyMove = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.updateRotation = false;
    }

    void Update()
    {
        if (isNavMeshExceptionNull)
        {
            if (isEnemyMove)
                Movement(Player.transform.position);
        }
    }

    public void Movement(Vector3 targetPosition)
    {
        if (isNavMeshExceptionNull)
        {
            isEnemyMove = true;
        }
        else
        {
            try
            {
                navMeshAgent.isStopped = false;
                navMeshAgent.SetDestination(targetPosition);
            }
            catch
            {
                isNavMeshExceptionNull = true;
            }
        }
    }

    public void StopMovement()
    {
        if (isNavMeshExceptionNull)
        {
            isEnemyMove = false;
        }
        else
        {
            try
            {
                navMeshAgent.isStopped = true;
            } catch
            {
                isNavMeshExceptionNull = true;
            }
        }
    }
}
