using UnityEngine;

public class PlayerClimbHandler : MonoBehaviour, IClimbHandler
{
    private float climbSpeed = 2f;
    private Vector2 climbDirection = new Vector2(0, 0);
    private bool isClimbing = false;
    private bool isClimbable = false;
    private Rigidbody2D rb2D;
    private void Start()
    {
        init();
    }
    void init()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    void IClimbHandler.EnterClimb()
    {
        rb2D.gravityScale = 0f;
        rb2D.velocity = Vector2.zero; // 避免殘留慣性
    }

    void IClimbHandler.ExitClimb()
    {
        rb2D.gravityScale = 2f; // 恢復原本重力
    }
    void IClimbHandler.ApplyClimb()
    {
        isClimbing = true;
        rb2D.velocity = new Vector2(0f, climbDirection.y * climbSpeed);
    }
    void IClimbHandler.StopClimb()
    {
        isClimbing = false;
        rb2D.velocity = Vector2.zero;
    }
    void IClimbHandler.SetClimbDirection(Vector2 climbDirection)
    {
        this.climbDirection = climbDirection;
    }
    void IClimbHandler.SetClimbSpeed(float climbSpeed)
    {
        this.climbSpeed = climbSpeed;
    }
    bool IClimbHandler.IsClimbing() => isClimbing;
    bool IClimbHandler.IsClimbable() => isClimbable;
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbable = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Ladder"))
        {
            isClimbable = false;
            rb2D.gravityScale = 2f; // 恢復原本重力
        }
    }
}
