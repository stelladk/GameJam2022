using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 20f;

    // Input Actions
    InputActions inputActions;
    bool isJumpPressed = false;
    bool isAttackPressed = false;

    // Controls
    CharacterController2D controller;
    private float inputX, inputY; 

    // Animations
    Animator animator;
    bool isWalking = false;
    bool isCrouching = false;

    // Combat
    PlayerCombat combat;

    void Awake()
    {
        animator = GetComponent<Animator>();
        combat = GetComponent<PlayerCombat>();
        controller = GetComponent<CharacterController2D>();
        inputActions = new InputActions();

        inputActions.Player.Move.started += OnMovementInput;
        inputActions.Player.Move.canceled += OnMovementInput;
        inputActions.Player.Move.performed += OnMovementInput;

        inputActions.Player.Jump.started += OnJumpInput;
        inputActions.Player.Jump.canceled += OnJumpInput;

        inputActions.Player.Attack.started += OnAttackInput;
        inputActions.Player.Attack.canceled += OnAttackInput;

        inputActions.Player.RangedAttack.started += OnRangedAttackInput;
        inputActions.Player.RangedAttack.canceled += OnRangedAttackInput;
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
        handleAnimations();
    }

    private void handleMovement()
    {
        Debug.Log(inputX);
        controller.Move(inputX * moveSpeed * Time.fixedDeltaTime, isCrouching, isJumpPressed);
    }

    private void handleAnimations()
    {
        animator.SetBool("isWalking", isWalking);
        animator.SetBool("isJumping", isJumpPressed);
        animator.SetBool("isCrouching", isCrouching);
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

    void OnAttackInput(InputAction.CallbackContext context)
    {
        isAttackPressed = context.ReadValueAsButton();
        if (isAttackPressed) {
            combat.MeleeAttack();
            animator.SetTrigger("MeleeAttack");
            isAttackPressed = false;
        }
    }

    void OnRangedAttackInput(InputAction.CallbackContext context)
    {
        isAttackPressed = context.ReadValueAsButton();
        if (isAttackPressed) {
            combat.RangedAttack();
            animator.SetTrigger("RangedAttack");
            isAttackPressed = false;
        }
    }
}
