using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBombFSM : MonoBehaviour
{
    [SerializeField] private List<AStateSO<EnemyBombFSM>> states = new List<AStateSO<EnemyBombFSM>>();

    [SerializeField] private AStateSO<EnemyBombFSM> currentState;
    [SerializeField] private int hp;
    private Slider hpSlider;

    public int Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyBombFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyBombFSM>> States { get => states; }

    private void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.maxValue = this.Hp;
        hpSlider.value = this.Hp;
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
        if (collision.gameObject.name.Contains("Bullet"))
        {
            Hit(1);
        }
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
        hpSlider.value = hp;

        if (hp <= 0)
        {
            GoToState<DieState>();
        }
    }
}
