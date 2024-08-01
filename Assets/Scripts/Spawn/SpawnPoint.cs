using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    private GameObject newPattern;
    public float spawnThresholdX = -30f;
    public float destroyThresholdX = -70f;

    public void Start()
    {
        SpawnObject();
    }

    private void Update()
    {
        if (newPattern != null)
        {
            Destroy(newPattern, 15.0f);

            // Check if the current pattern's x position is less than or equal to the spawn threshold
            if (newPattern.transform.position.x <= spawnThresholdX)
            {
                SpawnObject();
                
            }
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
}
