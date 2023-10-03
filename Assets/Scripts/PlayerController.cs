using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb2d;
    private Vector2 moveInput;

    [SerializeField]
    float Health;

    void Start()
    {
        
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();

        rb2d.velocity = moveInput * moveSpeed;
    }
}
