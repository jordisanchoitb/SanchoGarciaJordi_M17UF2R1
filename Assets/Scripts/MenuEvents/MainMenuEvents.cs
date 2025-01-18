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
        if (GameManager.gameManager != null)
        {
            GameManager.gameManager.GetComponent<BulletPool>().Clear();
            GameManager.gameManager.GetComponent<ObjectCoinPool>().Clear();
            GameManager.gameManager.GetComponent<ObjectKeyPool>().Clear();
        }
        if (RoomController.instance != null)
        {
            RoomController.instance.loadedRooms = new List<Room>();
            RoomController.instance.loadRoomQueue = new Queue<RoomInfo>();
            RoomController.instance.currentLoadRoomData = null;
        }
        SceneManager.LoadScene("FirstLevelMain");
        if (Player.player != null)
        {
            Player.player.gameObject.SetActive(true);
            Player.player.transform.position = Vector2.zero;
        }
        Door.OpenedDoors = 0;
        Player.IsPaused = false;
        Time.timeScale = 1;
    }
}
