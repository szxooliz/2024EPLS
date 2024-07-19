using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Reflection;

public class BirdJump : MonoBehaviour
{
    public float jumpPower = 13f;
    private int maxJumpCount = 2;
    public float jumpCheckRadius = 0.1f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private int jumpCount = 0;
    private bool isGrounded = true;
    private float timer;
    private float acceleration;
    private float initialGravity = 3.5f;
    [SerializeField] private float gravityIncreaseRate = 1f;
    private float maxGravity = 20f;
    private float currentGravity;

    public Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGravity = initialGravity;
        rb.gravityScale = currentGravity;
        acceleration = BackGroundLoop.acceleration;
        animator = GetComponent<Animator>();
    }

    // Update()에서 사용자 입력 처리
    private void Update() 
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }

        if(transform.position.y < -6)
        {
            Debug.Log("BirdJump ___ 목숨 : " + HealthManager.health);
            HealthManager.health = 0;
        }
    }

    // FixedUpdate()에서 물리 계산 처리
    private void FixedUpdate()
    {
        timer += Time.deltaTime;

        if (timer > acceleration && currentGravity <= maxGravity)
        {
            currentGravity += gravityIncreaseRate;
            rb.gravityScale = currentGravity;
            timer -= acceleration;
            jumpPower += 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            animator.SetTrigger("Run");

            jumpCount = 0;
            isGrounded = true;
        }
    }

    public void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            animator.SetTrigger("Jump");

            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
            isGrounded = false;
            //AudioManager.Instance.PlaySFX("Cat_Jump");
        }
    }
}
