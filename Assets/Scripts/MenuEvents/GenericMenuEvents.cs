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
        SceneManager.LoadScene("FirstLevelMain");
        Player.player.gameObject.SetActive(true);
        if (Player.player.GetComponentInChildren<BulletPool>() != null)
        {
            Player.player.GetComponentInChildren<BulletPool>().Clear();
        }
        if (Player.player.GetComponentInChildren<GrenadePool>() != null)
        {
            Player.player.GetComponentInChildren<GrenadePool>().Clear();
        }
        Player.inventory.ResetGettedWeapons();
        Player.IsPaused = false;
        Door.OpenedDoors = 0;
        Time.timeScale = 1;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
