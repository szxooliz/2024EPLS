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
            Debug.Log("ï¿½ï¿½ï¿½ï¿½ +1");
            //hyoju
            Coin.coin++;
            PlayerPrefs.SetInt("Coin", Coin.coin);
        }

        else if (collision.gameObject.CompareTag("LifePlus"))
        {
            Debug.Log("ï¿½ï¿½ï¿½ +1");
            if(HealthManager.health < 3) { 
                HealthManager.health++; 
            }
        }

        else if (collision.gameObject.CompareTag("ScoreMinus"))
        {
            Debug.Log("ï¿½ï¿½ï¿½ï¿½ -1");
        }

        else if (collision.gameObject.CompareTag("LifeMinus") || collision.gameObject.CompareTag("Pipe"))
        {
            HealthManager.health--;
            if(HealthManager.health <= 0 )
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
        }

        //Insert
        else if (collision.gameObject.CompareTag("ScorePlus"))
        {
            Debug.Log("Á¡¼ö +10");
            ScoreManager.scoreCount += 10;
        }
        //end

        if (!collision.gameObject.CompareTag("Pipe"))
        {
            Destroy(collision.gameObject);
        }
    }
}
