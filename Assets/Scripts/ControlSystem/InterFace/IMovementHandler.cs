public interface IMovementHandler
{
    public void ChangeDirection(float moveDIrection);
    public void ChangeMode(MoveMode moveMode);
    public void SetMoveSpeed(float moveSpeed);
    public void ApplyMovement();
}
