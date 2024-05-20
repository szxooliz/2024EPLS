using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
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
            GameObject newCoin = Instantiate(coin);
            newCoin.transform.position = new Vector3(10, Random.Range(-3.3f, 0), 0); // ���� ���� ���� ��ġ
            timeDiff = Random.Range(6.0f, 9.0f);
            timer = 0;
        }
    }
}
