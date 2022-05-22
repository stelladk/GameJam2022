using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float runSpeed = 40f;

    // Input Actions
    InputActions inputActions;
    bool isJumpPressed = false;
    bool isFiringPressed = false;

    // Controls
    CharacterController2D controller;
    private float inputX, inputY; 

    // Animations
    public Animator animator;
    bool isWalking = false;
    bool isJumping = false;
    bool isCrouching = false;

    void Awake()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController2D>();
        inputActions = new InputActions();

        inputActions.Player.Move.started += OnMovementInput;
        inputActions.Player.Move.canceled += OnMovementInput;
        inputActions.Player.Move.performed += OnMovementInput;

        inputActions.Player.Jump.started += OnJumpInput;
        inputActions.Player.Jump.canceled += OnJumpInput;

        inputActions.Player.Fire.started += OnFiringInput;
        inputActions.Player.Fire.canceled += OnFiringInput;
    }
    
    void OnEnable()
    {
        inputActions.Player.Enable();
    }

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void FixedUpdate()
    {
        handleMovement();
        handleFiring();
    }

    private void handleMovement()
    {
        Debug.Log(inputX);
        controller.Move(inputX * moveSpeed * Time.fixedDeltaTime, isCrouching, isJumpPressed);
    }

    private void handleFiring()
    {
        
    }

    // Input Callbacks
    void OnMovementInput(InputAction.CallbackContext context)
    {
        Vector2 currentMovementInput = context.ReadValue<Vector2>();
        inputX = currentMovementInput.x;
        inputY = currentMovementInput.y;

        if (inputX != 0) isWalking = true;
        else isWalking = false;

        if (inputY < 0) isCrouching = true;
        else isCrouching = false;
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void OnFiringInput(InputAction.CallbackContext context)
    {
        isFiringPressed = context.ReadValueAsButton();
    }
}
