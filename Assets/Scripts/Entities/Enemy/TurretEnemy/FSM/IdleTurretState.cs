using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "IdleTurretState", menuName = "ScriptableObjects/Enemy/TurretEnemy/IdleTurretState")]

public class IdleTurretState : AStateSO<EnemyTurretFSM>
{
    public override void OnStateEnter(EnemyTurretFSM entityController)
    {
    }

    public override void OnStateExit(EnemyTurretFSM entityController)
    {
    }

    public override void OnStateUpdate(EnemyTurretFSM entityController)
    {
    }
}
