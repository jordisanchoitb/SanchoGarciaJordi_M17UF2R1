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
        entityController.GetComponent<Animator>().SetBool("PlayerTriggered", true);
    }

    public override void OnStateExit(EnemyBombFSM entityController)
    {
        enemyMove.StopMovement();
        entityController.GetComponent<Animator>().SetBool("PlayerTriggered", false);
    }

    public override void OnStateUpdate(EnemyBombFSM entityController)
    {
        enemyMove.Movement(target.transform.position);        
    }
}