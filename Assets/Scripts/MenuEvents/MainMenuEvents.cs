using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuEvents : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void Update()
    {
       
    }

    public void PlayeLevelEightDirections()
    {
        SceneManager.LoadScene("LevelEightDirections");
    }

    public void PlayRogueLike()
    {
        SceneManager.LoadScene(1);
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
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
