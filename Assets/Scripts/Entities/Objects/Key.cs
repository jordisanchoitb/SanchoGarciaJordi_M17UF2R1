using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Key : MonoBehaviour
{
    private void OnDisable()
    {
        try
        {
            GameObject.Find("GameManager").GetComponent<ObjectKeyPool>().ReturnObject(gameObject);
        } catch (NullReferenceException)
        {
            Destroy(gameObject);
        }
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
