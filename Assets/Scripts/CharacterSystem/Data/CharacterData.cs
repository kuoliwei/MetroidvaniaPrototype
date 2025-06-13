using UnityEngine;
[CreateAssetMenu(fileName = "NewCharacterData", menuName = "Data/CharacterData")]
public class CharacterData : ScriptableObject
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpForce;
    public float climbSpeed;
    public int maxJumpTime;
    public float fastFallForce;
}
