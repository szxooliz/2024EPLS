using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private BirdJump birdJump;
    // Start is called before the first frame update
    void Start()
    {
        birdJump = FindObjectOfType<BirdJump>();
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
            CoinManager.coin++;
            PlayerPrefs.SetInt("Coin", CoinManager.coin);
            AudioManager.Instance.PlaySFX("Item_Heal");
        }

        else if (collision.gameObject.CompareTag("LifePlus"))
        {
            Debug.Log("���+1");
            if (HealthManager.health < 3)
            {
                HealthManager.health++;
                AudioManager.Instance.PlaySFX("Item_Heal");
            }
        }

        else if (collision.gameObject.CompareTag("ScoreMinus"))
        {
            Debug.Log("���� -1");
            AudioManager.Instance.PlaySFX("Item_Kill");
        }

        else if (collision.gameObject.CompareTag("Clover"))
        {
            StartCoroutine(KnockBack.instance.KnockBackCoroutine());
            Debug.Log("Clover");
        }

        else if (collision.gameObject.CompareTag("LifeMinus") || collision.gameObject.CompareTag("Pipe"))
        {
            HealthManager.health--;
            AudioManager.Instance.PlaySFX("Cat_Attack");
            AudioManager.Instance.PlaySFX("Item_Kill");
            if (HealthManager.health <= 0)
            {
                PlayerManager.isGameOver = true;
                gameObject.SetActive(false);
            }
        }

        else if (collision.gameObject.CompareTag("LifeMinus-2"))
        {
            HealthManager.health -= 2;
            AudioManager.Instance.PlaySFX("Cat_Attack");
            AudioManager.Instance.PlaySFX("Item_Kill");
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
            AudioManager.Instance.PlaySFX("Item_Heal");
        }

        else if (collision.gameObject.CompareTag("ScorePlus+15"))
        {
            Debug.Log("���� +15");
            ScoreManager.scoreCount += 15;
            AudioManager.Instance.PlaySFX("Item_Heal");
        }

        else if (collision.gameObject.CompareTag("ScorePlus+25"))
        {
            Debug.Log("���� +25");
            ScoreManager.scoreCount += 25;
            AudioManager.Instance.PlaySFX("Item_Heal");
        }
        //end

        if (!collision.gameObject.CompareTag("Pipe"))
        {
            Destroy(collision.gameObject);
        }
    }
}
