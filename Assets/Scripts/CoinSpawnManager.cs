using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
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
            GameObject newCoin = Instantiate(coin);
            newCoin.transform.position = new Vector3(10, Random.Range(-3.3f, 0), 0); // 랜덤 코인 생성 위치
            timeDiff = Random.Range(6.0f, 9.0f);
            timer = 0;
        }
    }
}
