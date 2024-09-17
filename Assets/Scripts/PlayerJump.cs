using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using System.Reflection;

public class PlayerJump : MonoBehaviour
{
    public float jumpPower = 13f;
    private int maxJumpCount = 2;
    public float jumpCheckRadius = 0.1f;
    public LayerMask groundLayer;
    private Rigidbody2D rb;
    private int jumpCount = 0;
    public static bool isGrounded = true;
    private float timer;
    private float acceleration;
    private float initialGravity = 3.5f;
    [SerializeField] private float gravityIncreaseRate = 1f;
    private float maxGravity = 20f;
    private float currentGravity;
    private Vector3 firstPosition = new Vector3 (0, 0, 0);
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGravity = initialGravity;
        rb.gravityScale = currentGravity;
        acceleration = BackGroundLoop.acceleration;
    }
    private void Update() 
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        //fallDown();
    }

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
            Player.Inst.animator.SetTrigger("Run");

            jumpCount = 0;
            isGrounded = true;       
        }
        else if (collision.gameObject.CompareTag("Clover"))
        {
            // 피격 애니메이션 발동
            Player.Inst.animator.SetTrigger("Jump");
        }  
        else if (collision.gameObject.CompareTag("DieBlock"))
        {
            Debug.Log("으앙 부딪힘!");
            Player.health = 0;
            HealthUI.Inst.UpdateHeartsUI();

            Debug.Log("PlayerJump-Update ___ SetGameOver");
            GameManager.Inst.SetGameOver();
        }
    }

    public void Jump()
    {
        if (jumpCount < maxJumpCount)
        {
            Player.Inst.animator.SetTrigger("Jump");

            rb.velocity = Vector2.zero;
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
            isGrounded = false;
            AudioManager.Instance.PlaySFX("Cat_Jump");
        }
    }

    public void WalkSound()
    {
        AudioManager.Instance.PlaySFX("Cat_Walk");
    }

    private void fallDown()
    {
        // 구멍으로 떨어지면 죽음
        // 얘가 리플레이가 안되는 문제의 원인임!! 얘가 리플레이하면 1 프레임을 가져와서 오류 발생
        if (transform.position.y < -6 && !GameManager.Inst.isGameOver)
        {
            Player.health = 0;
            HealthUI.Inst.UpdateHeartsUI();

            Debug.Log("PlayerJump-Update ___ SetGameOver");
            GameManager.Inst.SetGameOver();
        }
    }
}
