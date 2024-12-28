using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ExploteState", menuName = "ScriptableObjects/Enemy/BombEnemy/ExploteState")]

public class ExploteState : AStateSO<EnemyBombFSM>
{
    private const float TIME_WAIT = 1f;
    private Coroutine explosionCoroutine;

    public override void OnStateEnter(EnemyBombFSM entityController)
    {
        entityController.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        explosionCoroutine = entityController.StartCoroutine(Explote(entityController));
    }

    public override void OnStateExit(EnemyBombFSM entityController)
    {
        entityController.Hp = 0;
        if (explosionCoroutine != null)
        {
            entityController.StopCoroutine(explosionCoroutine);
        }
    }

    public override void OnStateUpdate(EnemyBombFSM entityController)
    {

    }

    private IEnumerator Explote(EnemyBombFSM entityController)
    {
        yield return new WaitForSeconds(TIME_WAIT);
        entityController.GoToState<DieState>();
        Debug.Log("Explote");
    }
}
