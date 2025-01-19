using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Door : MonoBehaviour
{
    public static int OpenedDoors = 0;
    [SerializeField] private int keysRequired = 1;
    private Animator animator;
    private bool isOpened = false;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpened)
        {
            GameEventsManager.gameEventsManager.DoorInteracted(keysRequired, OpenDoor);
        }
    }

    private void OpenDoor()
    {
        if (!isOpened)
        {
            isOpened = true;
            animator.SetTrigger("Open");
            GetComponent<BoxCollider2D>().enabled = false;
            OpenedDoors++;
            if (OpenedDoors == 3)
            {
                StartCoroutine(LoadWinMenu());
            } else
            {
                StartCoroutine(LoadNewLevel());
            } 
        }
    }

    private IEnumerator LoadWinMenu()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("WinMenu");
        Player.player.gameObject.SetActive(false);
    }

    private IEnumerator LoadNewLevel()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("FirstLevelMain");
        Player.player.transform.position = Vector2.zero;
        if (Player.player.GetComponentInChildren<BulletPool>() != null)
        {
            Player.player.GetComponentInChildren<BulletPool>().Clear();
        }
        if (Player.player.GetComponentInChildren<GrenadePool>() != null)
        {
            Player.player.GetComponentInChildren<GrenadePool>().Clear();
        }
        if (GameManager.gameManager.GetComponent<ObjectCoinPool>() != null)
        {
            GameManager.gameManager.GetComponent<ObjectCoinPool>().Clear();
        }
        if (GameManager.gameManager.GetComponent<ObjectKeyPool>() != null)
        {
            GameManager.gameManager.GetComponent<ObjectKeyPool>().Clear();
        }
        if (GameManager.gameManager.GetComponent<BulletPool>() != null)
        {
            GameManager.gameManager.GetComponent<BulletPool>().Clear();
        }
    }
}
