using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController: MonoBehaviour
{
    private Rigidbody2D rb;
    private float playerSpeed = 5.0f;
    private float jumpForce = 10.0f;
   


    public LayerMask groundLayer; // Layer mask to identify the ground

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Horizontal movement
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * playerSpeed, rb.velocity.y);

        // Flip the sprite based on movement direction
        if (moveInput > 0)
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        else if (moveInput < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Facing left

        // Check if the player is on the ground
      

        // Jump
        if (Input.GetButtonDown("Jump")) // && isgrounded should be added.
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
