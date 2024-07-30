using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Move instance;
    private float speed;

    private bool isPaused = false;

    [SerializeField] private float acceleration = 10f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        speed = BackGroundLoop.speed;
        acceleration = BackGroundLoop.acceleration;

        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.Log(isPaused);
        if (!isPaused)
        {
            speed = BackGroundLoop.speed;
            acceleration = BackGroundLoop.acceleration;
            transform.position += Vector3.left * (speed * Time.deltaTime);

            timer += Time.deltaTime;
            if (timer > acceleration)
            {
                speed += 2f;
                timer -= acceleration;
            }
        }
    }

    public void PauseMovement()
    {
        isPaused = true;
        Debug.Log("Move Paused");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        Debug.Log("Move Resumed");
    }
}
