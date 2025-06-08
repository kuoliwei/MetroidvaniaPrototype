using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpHandler : MonoBehaviour, IJumpHandler
{
    bool isGrounded;
    float jumpTimes;
    private int currentJumpCount = 0;
    float jumpforce;
    JumpMode jumpMode;
    Rigidbody2D rb2D;
    CapsuleCollider2D cc2D;
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
        cc2D = GetComponent<CapsuleCollider2D>();
    }
    void IJumpHandler.ApplyJump()
    {
        if (isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpforce);
            isGrounded = false; // 確保「按住跳鍵」不會一口氣跳好幾次
        }
    }
    void IJumpHandler.ChangeMode(JumpMode jumpMode)
    {
        this.jumpMode = jumpMode;
    }

    void IJumpHandler.SetJumpForce(float jumpForce)
    {
        this.jumpforce = jumpForce;
    }
    bool IJumpHandler.IsGrounded() => isGrounded;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Bounds bounds = cc2D.bounds;
            float footY = bounds.min.y + 0.05f; // 稍微高於腳底一點的容錯範圍
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // 接觸點是否在角色下方（可加一點偏移）
                if (contact.point.y <= footY)
                {
                    isGrounded = true;
                    return;
                }
            }
        }
        // 沒有任何一個接觸點來自地面方向
        isGrounded = false;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
        }
    }
}
