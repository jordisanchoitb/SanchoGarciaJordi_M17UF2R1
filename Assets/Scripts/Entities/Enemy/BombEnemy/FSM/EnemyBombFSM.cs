using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBombFSM : AEntity, IDrop
{
    [SerializeField] private List<AStateSO<EnemyBombFSM>> states = new List<AStateSO<EnemyBombFSM>>();

    [SerializeField] private AStateSO<EnemyBombFSM> currentState;
    [SerializeField] private int damage;
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private LayerMask damageableLayers;
    
    private GameObject gameManager;
    private Slider hpSlider;
    private string droppeable;

    public int Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyBombFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyBombFSM>> States { get => states; }

    private void OnEnable()
    {
        countCoints = Random.Range(1, 2);
        countKeys = Random.Range(1, 2);
        droppeable = Random.Range(0,2) % 2 == 0 ? "Key" : "Coin";
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
            GoToState<ChaseState>();
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

    public void Explote()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius, damageableLayers);

        foreach (var collider in colliders)
        {
            if (collider.TryGetComponent<IHurt>(out var enemy) && collider.gameObject.CompareTag("Player"))
            {
                enemy.Hurt(damage);
            }
        }

    }

    public override void Hurt(int damage)
    {
        hp -= damage;
        hpSlider.value = hp;

        if (hp <= 0)
        {
            GoToState<DieState>();
            Drop();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
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
