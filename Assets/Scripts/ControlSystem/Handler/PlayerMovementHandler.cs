using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMovementHandler : MonoBehaviour, IMovementHandler
{
    float moveSpeed;
    MoveDirection moveDirection;
    MoveMode moveMode;
    Rigidbody2D rb2D;
    // Start is called before the first frame update
    void Start()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void init()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }
    void IMovementHandler.ApplyMovement()
    {
        rb2D.velocity = new Vector2((int)moveDirection * moveSpeed, rb2D.velocity.y);
    }

    void IMovementHandler.ChangeDirection(MoveDirection moveDirection)
    {
        this.moveDirection = moveDirection;
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * (int)moveDirection;
        transform.localScale = scale;
    }

    void IMovementHandler.ChangeMode(MoveMode moveMode)
    {
        this.moveMode = moveMode;
    }

    void IMovementHandler.SetMoveSpeed(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
    }
}
