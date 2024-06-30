using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public static Tile instance;
    public float speed;
    private float startSpeed = 4f;

    public float tileWidth;
    private Vector3 startPosition;
    public static float timer;
    public static float acceleration = 10f;
    private bool isPaused = false;

    public Transform[] tiles; // 배열로 여러 배경을 담음

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        speed = startSpeed;
        startPosition = transform.position;
        tileWidth = tiles[0].GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (!isPaused)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].position += Vector3.left * speed * Time.deltaTime;

                // 배경 이미지가 화면 밖으로 나가면 재배치
                if (tiles[i].position.x < -tileWidth)
                {
                    tiles[i].position += new Vector3(tileWidth * tiles.Length, 0, 0);
                }
            }

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
        Debug.Log("Background paused");
    }

    public void ResumeMovement()
    {
        isPaused = false;
        Debug.Log("Background resumed");
    }
}


