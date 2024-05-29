using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private float speed;
    [SerializeField] private float acceleration = 10f;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        speed = BackGroundLoop.speed;
        acceleration = BackGroundLoop.acceleration;
    }

    // Update is called once per frame
    void Update()
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
