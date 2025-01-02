using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnDisable()
    {
        GameObject.Find("GameManager").GetComponent<ObjectCoinPool>().ReturnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollect>(out var player))
        {
            player.Collect(gameObject);
            gameObject.SetActive(false);
        }
    }
    
}
