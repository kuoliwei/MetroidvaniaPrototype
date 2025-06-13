using UnityEngine;
public interface IClimbHandler
{
    public bool IsClimbable();  // �O�_�b�i�k�����A
    public bool IsClimbing();                    // �O�_�B���k�����A
    public void EnterClimb();   // �i�J�k�����A
    public void ExitClimb();                     // ���}�k�����A
    public void SetClimbDirection(Vector2 climbDirection);
    public void SetClimbSpeed(float climbSpeed);
    public void ApplyClimb();       // �k������J��V�B�z
    public void StopClimb();       // �k������J��V�B�z
}
