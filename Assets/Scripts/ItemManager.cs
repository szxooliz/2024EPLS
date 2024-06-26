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
            Debug.Log("���� +1");
            //hyoju
            Coin.coin ++;
            PlayerPrefs.SetInt("Coin", Coin.coin);
        }

        else if (collision.gameObject.CompareTag("LifePlus"))
        {
            Debug.Log("��� +1");
            if(HealthManager.health < 3) { 
                HealthManager.health++;
                
            }
        }

        else if (collision.gameObject.CompareTag("ScoreMinus"))
        {
            Debug.Log("���� -1");
        }

        else if (collision.gameObject.CompareTag("LifeMinus") || collision.gameObject.CompareTag("Pipe"))
        {
            HealthManager.health--;
            
            if (HealthManager.health <= 0 )
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
        }

        else if (collision.gameObject.CompareTag("LifeMinus-2"))
        {
            HealthManager.health -= 2;
            
            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
        }

        //Insert
        else if (collision.gameObject.CompareTag("ScorePlus"))
        {
            Debug.Log("���� +10");
            ScoreManager.scoreCount += 10;
        }

        else if (collision.gameObject.CompareTag("ScorePlus+15"))
        {
            Debug.Log("���� +15");
            ScoreManager.scoreCount += 15;
        }

        else if (collision.gameObject.CompareTag("ScorePlus+25"))
        {
            Debug.Log("���� +25");
            ScoreManager.scoreCount += 25;
        }
        //end

        if (!collision.gameObject.CompareTag("Pipe"))
        {
            Destroy(collision.gameObject);
        }
    }
}
