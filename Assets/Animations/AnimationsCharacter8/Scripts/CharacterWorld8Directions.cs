using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterWorld8Directions : MonoBehaviour
{
    private void Start()
    {
        if (Player.player != null)
            Player.player.gameObject.SetActive(false);
            Time.timeScale = 1;
            Player.IsInventoryOpen = false;
            Player.IsPaused = false;
    }
}
