using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "DieTurretState", menuName = "ScriptableObjects/Enemy/TurretEnemy/DieTurretState")]
public class DieTurretState : AStateSO<EnemyTurretFSM>
{
    public override void OnStateEnter(EnemyTurretFSM entityController)
    {
        entityController.gameObject.SetActive(false);
    }

    public override void OnStateExit(EnemyTurretFSM entityController)
    {
    }

    public override void OnStateUpdate(EnemyTurretFSM entityController)
    {
    }
}
