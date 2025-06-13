using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 新增：使用 Input System
public class PlayerController : MonoBehaviour
{
    ICharacter player;
    IMovementHandler movementHandler;
    IJumpHandler jumpHandler;
    IClimbHandler climbHandler;
    PlayerInputActions inputActions;
    private void OnEnable()
    {
        inputActions = new();
        inputActions.Player.ApplyMovement.started += OnChangeDirection;
        inputActions.Player.ApplyJump.started += OnApplyJump;
        inputActions.Player.FastFall.started += OnFastFall;
        inputActions.Player.ApplyClimb.started += OnEnterClimb;
        inputActions.Player.ApplyClimb.canceled += OnStopClimb;
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.ApplyMovement.started -= OnChangeDirection;
        inputActions.Player.ApplyJump.started -= OnApplyJump;
        inputActions.Player.FastFall.started -= OnFastFall;
        inputActions.Player.ApplyClimb.started -= OnEnterClimb;
        inputActions.Player.ApplyClimb.canceled -= OnStopClimb;
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
        if (inputActions.Player.ApplyMovement.ReadValue<float>() != 0f)
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
        if (inputActions.Player.ApplyClimb.ReadValue<Vector2>() != new Vector2(0, 0))
        {
            if (climbHandler.IsClimbable())
            {
                climbHandler.SetClimbDirection(inputActions.Player.ApplyClimb.ReadValue<Vector2>());
                climbHandler.ApplyClimb();
            }
        }
    }
    void init()
    {
        GetPlayer();
        GetHandlers();
    }
    void GetPlayer()
    {
        player = GetComponent<Player>();
    }
    void GetHandlers()
    {
        movementHandler = GetComponent<PlayerMovementHandler>();
        jumpHandler = GetComponent<PlayerJumpHandler>();
        climbHandler = GetComponent<PlayerClimbHandler>();
        if (movementHandler == null)
            Debug.LogError("缺少 PlayerMovementHandler 元件");
        if (jumpHandler == null)
            Debug.LogError("缺少 PlayerJumpHandler 元件");
        if (climbHandler == null)
            Debug.LogError("缺少 PlayerClimbHandler 元件");
    }
    private void OnChangeDirection(InputAction.CallbackContext ctx)
    {
        float dirValue = ctx.ReadValue<float>();

        if (dirValue != 0)
        {
            movementHandler.ChangeDirection(dirValue);
            //Debug.Log($"方向：{dirValue}, 已設定速度");
        }
    }
    private void OnApplyJump(InputAction.CallbackContext ctx)
    {
        jumpHandler.SetJumpForce(player.CharacterData.jumpForce);
        jumpHandler.SetMaxJumpTime(player.CharacterData.maxJumpTime);
        jumpHandler.ApplyJump();
    }
    private void OnFastFall(InputAction.CallbackContext ctx)
    {
        jumpHandler.SetFastFallForce(player.CharacterData.fastFallForce);
        jumpHandler.ApplyFastFall();
    }
    private void OnEnterClimb(InputAction.CallbackContext ctx)
    {
        if (climbHandler.IsClimbable())
        {
            climbHandler.EnterClimb();
            climbHandler.SetClimbSpeed(player.CharacterData.climbSpeed);
        }
    }
    private void OnStopClimb(InputAction.CallbackContext ctx)
    {
        if (climbHandler.IsClimbing())
        {
            climbHandler.StopClimb();
        }
    }
}
