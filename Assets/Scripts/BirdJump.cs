using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

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

    private bool isKnockedBack = false;
    private float knockBackDistance = 2f;   // 뒤로 밀려나는 거리
    private float knockBackDuration = 0.5f;   // 밀려나는 시간

    private Camera mainCamera;
    private Vector3 cameraInitialPosition;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGravity = initialGravity;
        rb.gravityScale = currentGravity;
        acceleration = BackGroundLoop.acceleration;

        mainCamera = Camera.main;
        cameraInitialPosition = mainCamera.transform.position;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!isKnockedBack)
        {
            timer += Time.deltaTime;

            if (timer > acceleration && currentGravity <= maxGravity)
            {
                currentGravity += gravityIncreaseRate;
                rb.gravityScale = currentGravity;
                timer -= acceleration;
                jumpPower += 0.5f;
            }

            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                Jump();
            }
        }
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Ground")
        {
            jumpCount = 0;
            isGrounded = true;
        }
    }

    public IEnumerator KnockBack()
    {
        isKnockedBack = true;
        BackGroundLoop.instance.PauseMovement();
        Move.instance.PauseMovement();

        float elapsedTime = 0f;
        Vector3 originalPosition = transform.position;
        Vector3 targetPosition = transform.position - Vector3.right * knockBackDistance;

        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(originalPosition, targetPosition, elapsedTime/knockBackDuration);
            mainCamera.transform.position = cameraInitialPosition + (transform.position - originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(0.5f);

        elapsedTime = 0f;
        while (elapsedTime < knockBackDuration)
        {
            transform.position = Vector3.Lerp(targetPosition, originalPosition, elapsedTime/knockBackDuration);
            mainCamera.transform.position = cameraInitialPosition + (transform.position - originalPosition);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        isKnockedBack = false;
        BackGroundLoop.instance.ResumeMovement();
        Move.instance.ResumeMovement();
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
