using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Properties")]
    [SerializeField] private float speed = 10f;
    [SerializeField] private float damage = 5f;
    [SerializeField] private float lifeTime = 2f;

    private Vector2 direction;
    private float spawnTime;

    private void OnDisable()
    {
        GameObject.FindGameObjectWithTag("Rifle").GetComponentInChildren<BulletPool>().ReturnBullet(gameObject);
    }

    public void SetDirection(Vector2 initialDirection)
    {
        this.direction = initialDirection.normalized;
        this.spawnTime = Time.time;
    }


    void Update()
    {
        transform.Translate(this.direction * speed * Time.deltaTime, Space.World);

        if (Time.time - this.spawnTime >= this.lifeTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Contains("Enemy"))
        {
            Debug.Log("Hit Enemy");
        }

        gameObject.SetActive(false);
    }
}
