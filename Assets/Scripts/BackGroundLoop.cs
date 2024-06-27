using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public static BackGroundLoop instance;
    public static float speed;
    private static float startSpeed = 4f;

    private float backgroundWidth;
    private Vector3 startPosition;
    public static float timer;
    public static float acceleration = 10f;
    private bool isPaused = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        speed = startSpeed;
        startPosition = transform.position;
        backgroundWidth = (GetComponent<SpriteRenderer>().bounds.size.x)/3;
    }

    void Update()
    {
        if (!isPaused)
        {
            float newPosition = Mathf.Repeat(Time.time * speed, backgroundWidth);
            transform.position = startPosition + Vector3.left * newPosition;

            timer += Time.deltaTime;
            if (timer > acceleration)
            {
                speed += 0.5f;
                timer -= acceleration;
            }
        }
    }

    public void PauseMovement()
    {
        isPaused = true;
    }

    public void ResumeMovement()
    {
        isPaused = false;
    }
}
