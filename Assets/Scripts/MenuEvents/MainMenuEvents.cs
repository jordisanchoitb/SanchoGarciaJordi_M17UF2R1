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
        SceneManager.LoadScene("FirstLevelMain");
        if (Player.player != null)
        {
            Player.player.gameObject.SetActive(true);
            Player.player.transform.position = Vector2.zero;
            if (Player.player.GetComponentInChildren<BulletPool>() != null)
            {
                Player.player.GetComponentInChildren<BulletPool>().Clear();
            }
            if (Player.player.GetComponentInChildren<GrenadePool>() != null)
            {
                Player.player.GetComponentInChildren<GrenadePool>().Clear();
            }
            Player.inventory.ResetGettedWeapons();
            Player.player.GetComponent<WeaponManager>().ResetDefaultWeaponActive();
        }
        Door.OpenedDoors = 0;
        Player.IsPaused = false;
        Time.timeScale = 1;
    }
}
