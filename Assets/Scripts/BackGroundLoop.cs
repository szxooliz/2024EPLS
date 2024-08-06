using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    public static BackGroundLoop instance;
    public static float speed;
    public static float startSpeed;

    public float backgroundWidth;
    private Vector3 startPosition;
    public static float timer;
    public static float acceleration = 10f;
    public bool isPaused = false;

    public Transform[] backgrounds; // 배열로 여러 배경을 담음

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        timer = 0;
        startSpeed = 4f;
        speed = startSpeed;
        startPosition = transform.position;
        backgroundWidth = backgrounds[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (!isPaused)
        {
            for (int i = 0; i < backgrounds.Length; i++)
            {
                backgrounds[i].position += Vector3.left * speed * Time.deltaTime;

                // 배경 이미지가 화면 밖으로 나가면 재배치
                if (backgrounds[i].position.x < -backgroundWidth)
                {
                    backgrounds[i].position += new Vector3(backgroundWidth * backgrounds.Length, 0, 0);
                }
            }

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
        Debug.Log("Background paused");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        Debug.Log("Background resumed");
    }
}
