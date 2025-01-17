using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GenericMenuEvents : MonoBehaviour
{
    public void GoMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Replay()
    {
        Player.player.gameObject.SetActive(false);
        GameManager.gameManager.GetComponent<BulletPool>().Clear();
        GameManager.gameManager.GetComponent<ObjectCoinPool>().Clear();
        GameManager.gameManager.GetComponent<ObjectKeyPool>().Clear();
        RoomController.instance.loadedRooms = new List<Room>();
        RoomController.instance.loadRoomQueue = new Queue<RoomInfo>();
        RoomController.instance.currentLoadRoomData = null;
        SceneManager.LoadSceneAsync("FirstLevelMain");
        Player.player.gameObject.SetActive(true);
        Player.IsPaused = false;
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
