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
    private bool isGrounded = false;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        CheckGrounded();
        
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }
    }

    private void CheckGrounded()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, jumpCheckRadius, groundLayer);
        isGrounded = colliders.Length > 0;

        if (isGrounded)
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
        }
    }
}
