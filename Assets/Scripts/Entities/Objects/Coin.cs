using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    private void OnDisable()
    {
        try
        {
            GameObject.Find("GameManager").GetComponent<ObjectCoinPool>().ReturnObject(gameObject);
        } catch (NullReferenceException)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<ICollect>(out var player))
        {
            AudioManager.audioManager.PlaySoundEffectCollectCoin();
            player.Collect(gameObject);
            gameObject.SetActive(false);
        }
    }
    
}
