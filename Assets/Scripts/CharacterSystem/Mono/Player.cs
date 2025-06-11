using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerMovementHandler))]
[RequireComponent(typeof(PlayerJumpHandler))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class Player : MonoBehaviour, ICharacter
{
    [SerializeField] CharacterData characterData;
    public CharacterData CharacterData => characterData;

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
        Rigidbody2D rb2D = GetComponent<Rigidbody2D>();
        rb2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        rb2D.gravityScale = 2;
    }
}
