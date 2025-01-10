using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurretFSM : AEntity
{
    [SerializeField] private List<AStateSO<EnemyTurretFSM>> states = new List<AStateSO<EnemyTurretFSM>>();

    [SerializeField] private AStateSO<EnemyTurretFSM> currentState;

    private GameObject gameManager;
    private Slider hpSlider;
    private string droppeable;

    public float Hp { get => hp; set => hp = value; }
    
    public AStateSO<EnemyTurretFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyTurretFSM>> States { get => states; }

    private void OnEnable()
    {
        countCoints = Random.Range(1, 2);
        countKeys = Random.Range(0, 1);
        droppeable = Random.Range(0, 2) % 2 == 0 ? "Key" : "Coin";

    }

    private void Start()
    {
        hpSlider = GetComponentInChildren<Slider>();
        hpSlider.maxValue = this.Hp;
        hpSlider.value = this.Hp;
        gameManager = GameObject.Find("GameManager");
        
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

    public override void Hurt(float damage)
    {
        hp -= damage;
        hpSlider.value = hp;

        if (hp <= 0)
        {
            GoToState<DieTurretState>();
            Drop();

        }
    }

    public void Drop()
    {
        if (droppeable == "Coin")
        {
            for (int i = 0; i < countCoints; i++)
            {
                Vector3 positionRandom = transform.position +
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

                GameObject coin = gameManager.GetComponent<ObjectCoinPool>().GetObject();
                coin.transform.position = positionRandom;

            }
        }
        else if (droppeable == "Key")
        {
            for (int i = 0; i < countKeys; i++)
            {
                Vector3 positionRandom = transform.position +
                    new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);

                GameObject key = gameManager.GetComponent<ObjectKeyPool>().GetObject();
                key.transform.position = positionRandom;
            }
        }

    }
}
