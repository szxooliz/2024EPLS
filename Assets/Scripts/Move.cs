using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Move instance;
    private float speed;

    // private bool isPaused = false;

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
        if (!BackGroundLoop.instance.isPaused)
        {
            speed = BackGroundLoop.speed;
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }
        else
        {
            Debug.Log("Move 스크립트 업데이트문 if 정지");
        }
    }

    // public void PauseMovement()
    // {
    //     isPaused = true;
    //     Debug.Log(gameObject + " : Move Paused");
    // }

    // public void ResumeMovement()
    // {
    //     isPaused = false;
    //     Debug.Log("Move Resumed");
    // }
}
