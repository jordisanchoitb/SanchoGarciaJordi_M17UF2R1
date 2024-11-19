using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using Unity.VisualScripting;

public class InputManager : MonoBehaviour, PlayerControlers.IPlayerActions
{
    [SerializeField] private float speed;
    private PlayerControlers pControlers;
    private Rigidbody2D rigidBody;
    private Animator animator;
    private Vector2 inputMovement;

    private void Awake()
    {
        pControlers = new PlayerControlers();
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        pControlers.Player.SetCallbacks(this);
    }

    private void Update()
    {
        rigidBody.MovePosition(rigidBody.position + speed * Time.deltaTime * inputMovement.normalized);
        animator.SetFloat("velX", inputMovement.x);
        animator.SetFloat("velY", inputMovement.y);
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
}
