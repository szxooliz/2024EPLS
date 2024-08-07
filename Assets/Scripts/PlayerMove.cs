using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public static PlayerMove instance;
    private bool isMoving = false;
    private float speed;
    void Start()
    {
        instance = this;
        speed = BackGroundLoop.startSpeed;
    }

    void Update()
    {
        if (isMoving)   // 카메라와 플레이어가 움직여야하는 상태일 때
        {
            transform.position += Vector3.right * (speed * Time.deltaTime);
        }
    }

    public void PauseMovement()
    {
        isMoving = false;
    }

    public void ResumeMovement()
    {
        isMoving = true;
    }
}
