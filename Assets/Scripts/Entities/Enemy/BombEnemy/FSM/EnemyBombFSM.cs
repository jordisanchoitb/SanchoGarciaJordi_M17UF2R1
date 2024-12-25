using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombFSM : MonoBehaviour
{
    [SerializeField] private List<AStateSO<EnemyBombFSM>> states = new List<AStateSO<EnemyBombFSM>>();

    [SerializeField] private AStateSO<EnemyBombFSM> currentState;
    private int hp;

    public int Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyBombFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyBombFSM>> States { get => states; }

    private void Start()
    {
        GoToState<IdleState>();
        hp = 10;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<ChaseState>();
        if (collision.gameObject.CompareTag("Sword"))
        {
            Hit(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<IdleState>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<ExploteState>();
    }

    private void Update()
    {
        CurrentState.OnStateUpdate(this);
    }

    public void GoToState<t>() where t : AStateSO<EnemyBombFSM>
    {
        if (CurrentState.StatesToGo.Find(state => state is t))
        {
            CurrentState.OnStateExit(this);
            currentState = CurrentState.StatesToGo.Find(state => state is t);
            CurrentState.OnStateEnter(this);
        }
    }

    public void Hit(float damage)     
    {
        hp -= (int)damage;
        if (hp <= 0)
            GoToState<DieState>();
    }
}
