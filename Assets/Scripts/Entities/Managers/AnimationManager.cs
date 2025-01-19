using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    private Animator animator;
    private InputManager inputManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
    }



    private void FixedUpdate()
    {

        animator.SetFloat("velX", inputManager.inputMovement.x);
        animator.SetFloat("velY", inputManager.inputMovement.y);

        if (inputManager.inputMovement.x != 0 || inputManager.inputMovement.y != 0)
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
