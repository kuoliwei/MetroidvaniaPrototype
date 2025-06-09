using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.InputSystem; // 新增：使用 Input System
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
            Debug.LogError("缺少 PlayerMovementHandler 元件");

        if (jumpHandler == null)
            Debug.LogError("缺少 PlayerJumpHandler 元件");
    }
    private void OnChangeDirectionAndSpeed(InputAction.CallbackContext ctx)
    {
        float dirValue = ctx.ReadValue<float>();

        if (dirValue != 0)
        {
            MoveDirection direction = dirValue < 0 ? MoveDirection.left : MoveDirection.right;
            movementHandler.ChangeDirection(direction);
            Debug.Log($"方向：{direction},{dirValue}, 已設定速度");
        }
    }
    private void OnApplyJump(InputAction.CallbackContext ctx)
    {
        jumpHandler.SetJumpForce(player.CharacterData.jumpForce);
        jumpHandler.SetMaxJumpTime(2);
        jumpHandler.ApplyJump();
    }
}
