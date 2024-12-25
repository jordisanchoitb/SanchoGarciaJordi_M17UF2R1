using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DieState", menuName = "ScriptableObjects/Enemy/BombEnemy/DieState")]


public class DieState : AStateSO<EnemyBombFSM>
{
    public override void OnStateEnter(EnemyBombFSM enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public override void OnStateExit(EnemyBombFSM enemy)
    {
        enemy.gameObject.SetActive(false);
    }

    public override void OnStateUpdate(EnemyBombFSM enemy)
    {
        enemy.gameObject.SetActive(false);
    }
}
