using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerJumpHandler : MonoBehaviour, IJumpHandler
{
    bool isGrounded;
    int maxJumpTime;
    private int currentJumpCount = 0;
    float jumpforce;
    float fastFallForce;
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
    void IJumpHandler.ApplyFastFall()
    {
        // �Ȥ��\�b�Ť�Ĳ�o
        if (!isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, -Mathf.Abs(fastFallForce)); // �Ψ�L�T�w��
        }
    }
    void IJumpHandler.ApplyJump()
    {
        if (currentJumpCount < maxJumpTime && currentJumpCount > 0)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpforce);
            currentJumpCount++;
            Debug.Log(currentJumpCount);
        }
        else if (currentJumpCount == 0 && isGrounded)
        {
            rb2D.velocity = new Vector2(rb2D.velocity.x, jumpforce);
            currentJumpCount++;
            //Debug.Log(currentJumpCount);
        }
    }
    void IJumpHandler.SetMaxJumpTime(int maxJumpTime)
    {
        this.maxJumpTime = maxJumpTime;
    }
    void IJumpHandler.ChangeMode(JumpMode jumpMode)
    {
        this.jumpMode = jumpMode;
    }

    void IJumpHandler.SetFastFallForce(float fastFallForce)
    {
        this.fastFallForce = fastFallForce;
    }
    void IJumpHandler.SetJumpForce(float jumpForce)
    {
        this.jumpforce = jumpForce;
    }
    bool IJumpHandler.IsGrounded() => isGrounded;
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground" && !isGrounded)
        {
            Bounds bounds = cc2D.bounds;
            float footY = bounds.min.y + 0.05f; // �y�L����}���@�I���e���d��
            foreach (ContactPoint2D contact in collision.contacts)
            {
                // ��Ĳ�I�O�_�b����U��]�i�[�@�I�����^
                if (contact.point.y <= footY)
                {
                    //Debug.Log("touch ground");
                    isGrounded = true;
                    currentJumpCount = 0;
                    //Debug.Log(currentJumpCount);
                    return;
                }
            }
            Debug.Log("feet not touch ground");
            isGrounded = false;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            //Debug.Log("leave ground at OnCollisionExit");
            isGrounded = false;
        }
    }
}
