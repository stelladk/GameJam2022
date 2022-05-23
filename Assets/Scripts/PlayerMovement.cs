using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;

    // Input Actions
    InputHandler inputHandler;
    // InputActions inputActions;
    // bool isJumpPressed = false;
    // bool isAttackPressed = false;

    // Controls
    CharacterController2D controller;
    // private float inputX, inputY; 

    // Animations
    Animator animator;
    // bool isWalking = false;
    // bool isCrouching = false;

    // Combat
    PlayerCombat combat;

    void Awake()
    {
        animator = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
        controller = GetComponent<CharacterController2D>();
        inputHandler = GetComponent<InputHandler>();
    }

    void FixedUpdate()
    {
        handleMovement();
        handleAnimations();
    }

    private void handleMovement()
    {
        controller.Move(inputHandler.inputX * moveSpeed * Time.fixedDeltaTime, inputHandler.isCrouchingPressed, inputHandler.isJumpPressed);
    }

    private void handleAnimations()
    {
        animator.SetBool("isWalking", inputHandler.isWalkingPressed);
        animator.SetBool("isJumping", inputHandler.isJumpPressed);
        animator.SetBool("isCrouching", inputHandler.isCrouchingPressed);
    }

}
