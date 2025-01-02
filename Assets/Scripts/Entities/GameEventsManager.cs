using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager gameEventsManager;
    void Start()
    {
        if (gameEventsManager == null)
        {
            gameEventsManager = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public event Action OnKeyCollected;

    public void KeyCollected()
    {
        OnKeyCollected?.Invoke();
    }

    public event Action<int, Action> OnDoorInteracted;

    public void DoorInteracted(int keysRequired, Action onInteracted)
    {
        OnDoorInteracted?.Invoke(keysRequired, onInteracted);
    }
}
