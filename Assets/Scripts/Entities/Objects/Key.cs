using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnDisable()
    {
        GameObject.Find("GameManager").GetComponent<ObjectKeyPool>().ReturnObject(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollect>(out var player))
        {
            GameEventsManager.gameEventsManager.KeyCollected();
            player.Collect(gameObject);
            gameObject.SetActive(false);
        }
    }
}
