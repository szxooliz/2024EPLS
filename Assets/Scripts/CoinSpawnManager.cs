using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject coin;
    private float timeDiff = Random.Range(6.0f, 9.0f); // ������ ������ ������ �ð�
    private float timer = 0; // ���� �ð�
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeDiff)
        {
            GameObject newCoin = Instantiate(coin);
            newCoin.transform.position = new Vector3(10, Random.Range(-3.15f, 0), 0); // ���� ���� ���� ��ġ
            timeDiff = Random.Range(6.0f, 9.0f);
            timer = 0;
            Destroy(newCoin, 11.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
}
