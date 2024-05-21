using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs; // 아이템 프리팹을 담을 리스트
    private float timeDiff; // 아이템 나오는 랜덤한 시간
    private float timer = 0; // 현재 시간
    
    // Start is called before the first frame update
    void Start()
    {
        timeDiff = Random.Range(6.0f, 9.0f);
    }

    // Update is called once per frame
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
            randomItem.transform.position = new Vector3(10, Random.Range(-3.3f, 0), 0);
        }
    }
}
