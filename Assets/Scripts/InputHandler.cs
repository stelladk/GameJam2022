using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    // Input Actions
    InputActions inputActions;
    [HideInInspector]
    public float inputX, inputY;
    [HideInInspector]
    public bool isWalkingPressed = false;
    [HideInInspector]
    public bool isCrouchingPressed = false;
    [HideInInspector]
    public bool isJumpPressed = false;
    [HideInInspector]
    public bool isAttackPressed = false;
    [HideInInspector]
    public bool isRangedAttackPressed = false;


    void Awake()
    {
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

    // Input Callbacks
    void OnMovementInput(InputAction.CallbackContext context)
    {
        Vector2 currentMovementInput = context.ReadValue<Vector2>();
        inputX = currentMovementInput.x;
        inputY = currentMovementInput.y;

        if (inputX != 0) isWalkingPressed = true;
        else isWalkingPressed = false;

        if (inputY < 0) isCrouchingPressed = true;
        else isCrouchingPressed = false;
    }

    void OnJumpInput(InputAction.CallbackContext context)
    {
        isJumpPressed = context.ReadValueAsButton();
    }

    void OnAttackInput(InputAction.CallbackContext context)
    {
        isAttackPressed = context.ReadValueAsButton();
    }

    void OnRangedAttackInput(InputAction.CallbackContext context)
    {
        isRangedAttackPressed = context.ReadValueAsButton();
    }
}
