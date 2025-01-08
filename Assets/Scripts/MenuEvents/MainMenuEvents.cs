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
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
