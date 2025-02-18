using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurretFSM : AEntity
{
    [SerializeField] private List<AStateSO<EnemyTurretFSM>> states = new List<AStateSO<EnemyTurretFSM>>();

    [SerializeField] private AStateSO<EnemyTurretFSM> currentState;

    private GameObject gameManager;
    private string droppeable;

    public GameObject hpSlider;
    public bool notInRoom = false;
    public float Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyTurretFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyTurretFSM>> States { get => states; }

    private void OnEnable()
    {
        countCoints = Random.Range(1, 4);
        countKeys = Random.Range(1, 3);
        droppeable = Random.Range(0, 2) % 2 == 0 ? "Key" : "Coin";
        if (hpSlider != null)
            hpSlider.SetActive(false);

    }

    private void Start()
    {
        hpSlider.SetActive(false);
        hpSlider.GetComponent<Slider>().maxValue = this.Hp;
        hpSlider.GetComponent<Slider>().value = this.Hp;
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
        hpSlider.SetActive(true);
        hp -= damage;
        hpSlider.GetComponent<Slider>().value = hp;

        if (hp <= 0)
        {
            Drop();
            GoToState<DieTurretState>();
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
