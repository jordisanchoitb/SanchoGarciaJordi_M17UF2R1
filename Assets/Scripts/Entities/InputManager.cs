using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour, PlayerControlers.IPlayerActions, IMovement
{
    [Header("Attributes")]
    [SerializeField] private float speed;
    private PlayerControlers pControlers;
    private Rigidbody2D rigidBody;
    [NonSerialized] public Vector2 inputMovement;

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
        rigidBody.MovePosition(rigidBody.position + speed * Time.deltaTime * inputMovement.normalized);
    }

    public void OnInventory(InputAction.CallbackContext context)
    {
        SceneManager.LoadScene("InventoryScene", LoadSceneMode.Additive);
        Time.timeScale = 0;
    }
}
