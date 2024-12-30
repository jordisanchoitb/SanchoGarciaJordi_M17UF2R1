using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurretFSM : AEntity
{
    [SerializeField] private List<AStateSO<EnemyTurretFSM>> states = new List<AStateSO<EnemyTurretFSM>>();

    [SerializeField] private AStateSO<EnemyTurretFSM> currentState;
    private Slider hpSlider;
    public int Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyTurretFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyTurretFSM>> States { get => states; }

    private void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.maxValue = this.Hp;
        hpSlider.value = this.Hp;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<ShotTurretState>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            GoToState<IdleTurretState>();
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

    public override void Hurt(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;

        if (hp <= 0)
        {
            GoToState<DieTurretState>();
        }
    }
}
