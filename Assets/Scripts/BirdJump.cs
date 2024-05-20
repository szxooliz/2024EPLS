using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdJump : MonoBehaviour
{
    public float jumpPower = 8f;
    private int maxJumpCount = 2;
    public float jumpCheckRadius = 0.1f;
    public LayerMask groundLayer;
    
    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded = true;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            jumpCount = 0;
        }
    }

    public void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
            isGrounded = false;
        }
    }
}
