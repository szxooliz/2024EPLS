using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] prefabToSpawn;
    public float spawnThreshold;
    public float destroyThreshold;

    private GameObject ptn1;
    private GameObject ptn2;

    public void Start()
    {
        ptn1 = SpawnObject();
    }

    private void Update()
    {
        if (ptn1 != null && ptn1.transform.position.x <= spawnThreshold && ptn2 == null)
        {
            ptn2 = SpawnObject();
        }

        if (ptn1 != null && ptn1.transform.position.x <= destroyThreshold)
        {
            Destroy(ptn1);
        }

        if (ptn2 != null)
        {
            if (ptn2.transform.position.x <= spawnThreshold && ptn1 == null)
            {
                ptn1 = SpawnObject();
            }

            if (ptn2.transform.position.x <= destroyThreshold)
            {
                Destroy(ptn2);
            }
        }
    }

    public GameObject SpawnObject()
    {
        if (prefabToSpawn != null && prefabToSpawn.Length > 0)
        {
            int index = Random.Range(0, prefabToSpawn.Length);
            GameObject newPattern = Instantiate(prefabToSpawn[index], transform.position, Quaternion.identity);
            return newPattern;
        }
        return null;
    }
}
