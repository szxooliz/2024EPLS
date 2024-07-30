using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public static CameraMove instance;
    private bool isMoving = false;
    private float speed;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        speed = BackGroundLoop.startSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving) {
            transform.position += Vector3.right * speed * Time.deltaTime;
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
