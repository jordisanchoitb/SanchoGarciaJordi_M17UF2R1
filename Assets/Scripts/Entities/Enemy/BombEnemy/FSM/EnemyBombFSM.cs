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
    private string droppeable;
    
    public GameObject hpSlider;
    public bool notInRoom = false;
    public float Hp { get => hp; set => hp = value; }
    public AStateSO<EnemyBombFSM> CurrentState { get => currentState; }
    public List<AStateSO<EnemyBombFSM>> States { get => states; }

    private void OnEnable()
    {
        countCoints = Random.Range(1, 4);
        countKeys = Random.Range(1, 3);
        droppeable = Random.Range(0,2) % 2 == 0 ? "Key" : "Coin";
        GoToState<IdleState>();
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

    public override void Hurt(float damage)
    {
        hpSlider.SetActive(true);
        hp -= damage;
        hpSlider.GetComponent<Slider>().value = hp;

        if (hp <= 0)
        {
            Drop();
            GoToState<DieState>();
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
