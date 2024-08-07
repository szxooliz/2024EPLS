using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs;
    private float timeDiff;
    private float timer = 0;
    
    void Start()
    {
        timeDiff = Random.Range(6.0f, 9.0f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeDiff)
        {
            SpawnRandomItem();
            timeDiff = Random.Range(6.0f, 9.0f);
            timer = 0;
        }
    }

    void SpawnRandomItem()
    {
        if (itemPrefabs.Count > 0)
        {
            int randomIdx = Random.Range(0, itemPrefabs.Count);
            GameObject randomItem = Instantiate(itemPrefabs[randomIdx]);
            randomItem.transform.position = new Vector3(10, Random.Range(-2f, 0), 0);
        }
    }
}
