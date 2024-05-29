using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public static float speed = 4f;

    private float backgroundWidth;
    private Vector3 startPosition;
    private float timer;
    public static float acceleration = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
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
