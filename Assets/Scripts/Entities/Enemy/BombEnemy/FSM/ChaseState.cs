using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ChaseState", menuName = "ScriptableObjects/Enemy/BombEnemy/ChaseState")]

public class ChaseState : AStateSO<EnemyBombFSM>
{
    private GameObject target;
    private EnemyMove enemyMove;
    public override void OnStateEnter(EnemyBombFSM entityController)
    {
        target = GameObject.Find("Player");
        enemyMove = entityController.GetComponent<EnemyMove>();                
    }

    public override void OnStateExit(EnemyBombFSM entityController)
    {
        enemyMove.StopMovement();
    }

    public override void OnStateUpdate(EnemyBombFSM entityController)
    {
        enemyMove.Movement(target.transform.position);        
    }
}