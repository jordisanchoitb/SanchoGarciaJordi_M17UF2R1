using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMove : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject Player;
    private bool isUsingNavMesh = true;
    private bool isEnemyMove = false;
    private float agentSpeed;
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();

        if (navMeshAgent != null)
        {
            navMeshAgent.updateUpAxis = false;
            navMeshAgent.updateRotation = false;

            agentSpeed = navMeshAgent.speed;
        }
    }

    void Update()
    {
        if (isEnemyMove && !isUsingNavMesh)
        {
            transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, agentSpeed * Time.deltaTime);
        }
    }

    public void Movement(Vector3 targetPosition)
    {
        if (!isUsingNavMesh)
        {
            isEnemyMove = true;
        }
        else
        {
            if (IsNavMeshAgentValid())
            {
                try
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(targetPosition);
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error en NavMeshAgent: {ex.Message}. Cambiando a MoveTowards.");
                    SwitchToMoveTowards();
                }
            }
            else
            {
                Debug.LogWarning("NavMeshAgent no válido. Cambiando a MoveTowards.");
                SwitchToMoveTowards();
            }
        }
    }

    public void StopMovement()
    {
        if (!isUsingNavMesh)
        {
            isEnemyMove = false;
        }
        else
        {
            if (IsNavMeshAgentValid())
            {
                try
                {
                    navMeshAgent.isStopped = true;
                }
                catch (System.Exception ex)
                {
                    Debug.LogError($"Error al detener NavMeshAgent: {ex.Message}. Cambiando a MoveTowards.");
                    SwitchToMoveTowards();
                }
            }
            else
            {
                Debug.LogWarning("NavMeshAgent no válido al intentar detener. Cambiando a MoveTowards.");
                SwitchToMoveTowards();
            }
        }
    }

    private bool IsNavMeshAgentValid()
    {
        return navMeshAgent != null && navMeshAgent.isActiveAndEnabled && navMeshAgent.isOnNavMesh;
    }

    private void SwitchToMoveTowards()
    {
        isUsingNavMesh = false;
        isEnemyMove = true;

        if (navMeshAgent != null)
        {
            agentSpeed = navMeshAgent.speed;
        } else if (navMeshAgent == null)
        {
            agentSpeed = 2f;
        }
    }
}
