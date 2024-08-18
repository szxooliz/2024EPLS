using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public static Move instance;
    private float speed;

    [SerializeField] private float acceleration = 10f;
    private float timer;

    void Start()
    {
        speed = BackGroundLoop.speed;
        acceleration = BackGroundLoop.acceleration;

        instance = this;
    }

    void Update()
    {
        if (!BackGroundLoop.instance.isPaused)
        {
            speed = BackGroundLoop.speed;
            transform.position += Vector3.left * (speed * Time.deltaTime);
        }
    }
}
