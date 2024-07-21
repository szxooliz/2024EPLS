using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    public float repeatInterval;

    private GameObject newPattern;
    public void Start()
    {
        if(repeatInterval > 0)
        {
            InvokeRepeating("SpawnObject", 0.0f, repeatInterval);
        }
    }
    private void Update()
    {
        DestroyPattern();
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
