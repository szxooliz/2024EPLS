using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Tile : MonoBehaviour
{
    public SpriteRenderer[] tiles;
    private float startSpeed = 4f;
    private bool isPaused = false;
    private void Start()
    {
        BackGroundLoop.speed = startSpeed;
        
    }
    private void Update()
    {
        if (!isPaused)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                tiles[i].transform.Translate(new Vector2(-1, 0) * Time.deltaTime * BackGroundLoop.speed);

                if (tiles[i].transform.position.x < -20)
                {
                    Vector2 newPosition = tiles[i].transform.position;
                    newPosition.x = 10;
                    tiles[i].transform.position = newPosition;
                }
            }

            

            BackGroundLoop.timer += Time.deltaTime;
            if (BackGroundLoop.timer > BackGroundLoop.acceleration)
            {
                BackGroundLoop.speed += 0.5f;
                BackGroundLoop.timer -= BackGroundLoop.acceleration;
            }
        }
        
    }
}