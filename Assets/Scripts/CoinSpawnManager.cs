using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs; // ������ �������� ���� ����Ʈ
    private float timeDiff; // ������ ������ ������ �ð�
    private float timer = 0; // ���� �ð�
    
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
            randomItem.transform.position = new Vector3(10, Random.Range(-2f, 0), 0);
        }
    }
}
