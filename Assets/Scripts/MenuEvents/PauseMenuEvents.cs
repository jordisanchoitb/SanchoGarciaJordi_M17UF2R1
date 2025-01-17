using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuEvents : MonoBehaviour
{
    public void ReturnGame()
    {
        Time.timeScale = 1;
        SceneManager.UnloadSceneAsync("PauseMenuScene");
        Player.IsPaused = false;
    }
}

