using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ExploteState", menuName = "ScriptableObjects/Enemy/BombEnemy/ExploteState")]

public class ExploteState : AStateSO<EnemyBombFSM>
{

    public override void OnStateEnter(EnemyBombFSM entityController)
    {
        entityController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        entityController.GetComponent<Animator>().SetTrigger("Explosion");
    }

    public override void OnStateExit(EnemyBombFSM entityController)
    {
        entityController.Hp = 0;
        entityController.Explote();
    }

    public override void OnStateUpdate(EnemyBombFSM entityController)
    {


    }
}
