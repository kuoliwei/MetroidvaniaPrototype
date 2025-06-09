public interface IJumpHandler
{
    public bool IsGrounded();
    public void ChangeMode(JumpMode jumpMode);
    public void SetJumpForce(float jumpForce);
    public void SetMaxJumpTime(int maxJumpTime);
    public void ApplyJump();
}
