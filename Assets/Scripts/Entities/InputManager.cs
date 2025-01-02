using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour, PlayerControlers.IPlayerActions, IMovement
{
    [Header("Attributes")]
    [SerializeField] private float speed;
    private PlayerControlers pControlers;
    private Rigidbody2D rigidBody;
    [NonSerialized] public Vector2 inputMovement;
    public bool isInventoryOpen;
    public bool isPauseMenuOpen;

    private void Awake()
    {
        pControlers = new PlayerControlers();
        rigidBody = GetComponent<Rigidbody2D>();
        pControlers.Player.SetCallbacks(this);
    }
    private void FixedUpdate()
    {
        Movement();
    }

    private void OnEnable()
    {
        pControlers.Enable();
    }

    private void OnDisable()
    {
        pControlers.Disable();
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        inputMovement = context.ReadValue<Vector2>();
    }

    public void Movement()
    {
        if (isInventoryOpen)
            return;
        rigidBody.MovePosition(rigidBody.position + speed * Time.deltaTime * inputMovement.normalized);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isInventoryOpen)
            {
                SceneManager.UnloadSceneAsync("InventoryScene");
                Time.timeScale = 1;
                isInventoryOpen = false;
            }
            else
            {
                SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
                Time.timeScale = 0;
                isInventoryOpen = true;
            }
        }
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (isInventoryOpen)
            {
                SceneManager.UnloadSceneAsync("InventoryScene");
                Time.timeScale = 1;
                isInventoryOpen = false;
            } else
            {
                if (isPauseMenuOpen)
                {
                    SceneManager.UnloadSceneAsync("PauseMenuScene");
                    Time.timeScale = 1;
                    isPauseMenuOpen = false;
                }
                else
                {
                    SceneManager.LoadScene("PauseMenuScene", LoadSceneMode.Additive);
                    Time.timeScale = 0;
                    isPauseMenuOpen = true;
                }
            }
        }
    }
}
