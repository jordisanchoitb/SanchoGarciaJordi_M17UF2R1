using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletPrefab;
    private Stack<GameObject> bullets;

    private void Awake()
    {
        this.bullets = new Stack<GameObject>();
    }

    public GameObject GetBullet()
    {
        if (this.bullets.Count > 0)
        {
            GameObject bullet = this.bullets.Pop();
            bullet.SetActive(true);
            return bullet;
        }
        else
        {
            return Instantiate(this.bulletPrefab);
        }
    }

    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        this.bullets.Push(bullet);
    }

    public void Clear()
    {
        this.bullets.Clear();
    }
}
