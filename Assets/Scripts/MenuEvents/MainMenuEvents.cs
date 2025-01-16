using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    public void PlayeLevelEightDirections()
    {
        SceneManager.LoadScene("LevelEightDirections");
    }

    public void PlayRogueLike()
    {
        SceneManager.LoadScene("FirstLevelMain");
        if (Player.player != null)
        {
            Player.player.gameObject.SetActive(true);
        }
        if (GameManager.gameManager != null)
        {
            GameManager.gameManager.GetComponent<BulletPool>().Clear();
            GameManager.gameManager.GetComponent<ObjectCoinPool>().Clear();
            GameManager.gameManager.GetComponent<ObjectKeyPool>().Clear();
        }
        if (RoomController.instance != null)
        {
            RoomController.instance.loadedRooms = new List<Room>();
        }
        Player.IsPaused = false;
        Time.timeScale = 1;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
