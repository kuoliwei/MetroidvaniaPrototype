using UnityEngine;
public interface IClimbHandler
{
    public bool IsClimbable();  // 是否在可攀爬狀態
    public bool IsClimbing();                    // 是否處於攀爬狀態
    public void EnterClimb();   // 進入攀爬狀態
    public void ExitClimb();                     // 離開攀爬狀態
    public void SetClimbDirection(Vector2 climbDirection);
    public void SetClimbSpeed(float climbSpeed);
    public void ApplyClimb();       // 攀爬中輸入方向處理
    public void StopClimb();       // 攀爬中輸入方向處理
}
