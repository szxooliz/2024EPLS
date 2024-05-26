using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("内牢 +1");
            //捞瓤林 火涝
            ScoreManager.scoreCount += 1;
            //火涝 场
        }

        else if (collision.gameObject.CompareTag("LifePlus"))
        {
            Debug.Log("格见 +1");
        }

        else if (collision.gameObject.CompareTag("ScoreMinus"))
        {
            Debug.Log("痢荐 -1");
            ScoreManager.scoreCount -= 1;
        }

        else if (collision.gameObject.CompareTag("LifeMinus"))
        {
            HealthManager.health--;
            if(HealthManager.health <= 0 )
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
        }
        Destroy(collision.gameObject);
    }
}
