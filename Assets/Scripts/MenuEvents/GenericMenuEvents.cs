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

    }
    public void ExitGame()
    {
        Application.Quit();
    }

}
