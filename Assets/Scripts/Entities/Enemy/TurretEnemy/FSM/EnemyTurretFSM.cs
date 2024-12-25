using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurretFSM : MonoBehaviour
{
    [SerializeField] private List<AStateSO<EnemyTurretFSM>> states = new List<AStateSO<EnemyTurretFSM>>();

    [SerializeField] private AStateSO<EnemyTurretFSM> currentState;
    private int hp;

    public int Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyTurretFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyTurretFSM>> States { get => states; }

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<ShotTurretState>();
        if (collision.gameObject.CompareTag("Sword"))
        {
            Hit(1);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<IdleTurretState>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Proyectile"))
        {
            Hit(1);
        }
            
    }

    private void Update()
    {
        CurrentState.OnStateUpdate(this);
    }

    public void GoToState<t>() where t : AStateSO<EnemyTurretFSM>
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
        {
            GoToState<DieTurretState>();
        }
    }
}
