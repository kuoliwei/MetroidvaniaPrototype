using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem; // �s�W�G�ϥ� Input System
public class PlayerController : MonoBehaviour
{
    ICharacter player;
    IMovementHandler movementHandler;
    IJumpHandler jumpHandler;
    PlayerInputActions inputActions;
    private void OnEnable()
    {
        inputActions = new();
        inputActions.Player.ChangeDirectionAndSpeed.performed += OnChangeDirectionAndSpeed;
        inputActions.Player.ApplyJump.performed += OnApplyJump;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.ChangeDirectionAndSpeed.performed -= OnChangeDirectionAndSpeed;
        inputActions.Player.ApplyJump.performed -= OnApplyJump;
        inputActions.Disable();
    }
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputActions.Player.ApplyMovement.ReadValue<float>() > 0f)
        {
            movementHandler.ApplyMovement();
        }
        if (inputActions.Player.SetRunSpeen.ReadValue<float>() > 0f)
        {
            movementHandler.SetMoveSpeed(player.CharacterData.runSpeed);
        }
        else
        {
            movementHandler.SetMoveSpeed(player.CharacterData.walkSpeed);
        }
    }
    void init()
    {
        GetPlayer();
        GetHandlers();
    }
    void GetPlayer()
    {
        this.player = GetComponent<Player>();
    }
    void GetHandlers()
    {
        this.movementHandler = GetComponent<PlayerMovementHandler>();
        this.jumpHandler = GetComponent<PlayerJumpHandler>();
        if (movementHandler == null)
            Debug.LogError("�ʤ� PlayerMovementHandler ����");

        if (jumpHandler == null)
            Debug.LogError("�ʤ� PlayerJumpHandler ����");
    }
    private void OnChangeDirectionAndSpeed(InputAction.CallbackContext ctx)
    {
        float dirValue = ctx.ReadValue<float>();

        if (dirValue != 0)
        {
            MoveDirection direction = dirValue < 0 ? MoveDirection.left : MoveDirection.right;
            movementHandler.ChangeDirection(direction);
            Debug.Log($"��V�G{direction},{dirValue}, �w�]�w�t��");
        }
    }
    private void OnApplyJump(InputAction.CallbackContext ctx)
    {
        jumpHandler.SetJumpForce(player.CharacterData.jumpForce);
        jumpHandler.SetMaxJumpTime(2);
        jumpHandler.ApplyJump();
    }
}
