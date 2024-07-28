using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    public float repeatInterval;
    private GameObject newPattern;
    private float speed;
    [SerializeField] private float acceleration = 10f;
    private float timer;
    private float spawnTimer;

    public void Start()
    {
        speed = BackGroundLoop.speed;
        acceleration = BackGroundLoop.acceleration;
        repeatInterval = 48 / speed;
    }

    private void Update()
    {
        speed = BackGroundLoop.speed;
        acceleration = BackGroundLoop.acceleration;
        DestroyPattern();

        timer += Time.deltaTime;
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= repeatInterval)
        {
            SpawnObject();
            spawnTimer = 0f;  // Reset the spawn timer
        }

        if (timer > acceleration)
        {
            speed += 2f;
            repeatInterval = 48 / speed;
            timer -= acceleration;
        }
    }

    public GameObject SpawnObject()
    {
        if (prefabToSpawn != null && prefabToSpawn.Length > 0)
        {
            int index = Random.Range(0, prefabToSpawn.Length);
            newPattern = Instantiate(prefabToSpawn[index], transform.position, Quaternion.identity);
            return newPattern;
        }
        return null;
    }

    public void DestroyPattern()
    {
        Destroy(newPattern, 11.0f);
    }
}
