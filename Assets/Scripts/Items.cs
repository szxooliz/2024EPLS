using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    // 목숨 관련 아이템
    private const string ITEM_TUNA = "LifePlus";
    private const string ITEM_ROTTEN_FISH = "LifeMinus";
    private const string ITEM_ROTTEN_TUNA = "LifeMinus-2";

    // 점수 관련 아이템
    private const string ITEM_FEATHER = "ScorePlus";
    private const string ITEM_MOUSE = "ScorePlus+15";
    private const string ITEM_FISH_DOLL = "ScorePlus+25";


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 해당 아이템이 플레이어와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag(ITEM_TUNA))
            {
                HandleTunaCollision();
            }
            else if (gameObject.CompareTag(ITEM_ROTTEN_FISH))
            {
                HandleRottenItemCollision(1);
            }
            else if (gameObject.CompareTag(ITEM_ROTTEN_TUNA))
            {
                HandleRottenItemCollision(2);
            }
            else if (gameObject.CompareTag(ITEM_FEATHER))
            {
                HandleScoreItemCollision(10);
            }
            else if (gameObject.CompareTag(ITEM_MOUSE))
            {
                HandleScoreItemCollision(15);
            }
            else if (gameObject.CompareTag(ITEM_FISH_DOLL))
            {
                HandleScoreItemCollision(25);
            }

            // 해당 아이템 제거
            Destroy(gameObject);
        }
    }
    /// <summary>
    /// 참치캔: 목숨 + 1
    /// </summary>
    private void HandleTunaCollision()
    {
        // Debug.Log("목숨+1");
        if (Player.health < 3)
        {
            Player.health++;
            HealthUI.Inst.UpdateHeartsUI();
            // AudioManager.Instance.PlaySFX("Item_Heal");
        }
    }

    /// <summary>
    /// 썩은 생선, 참치캔: 목숨 감소
    /// </summary>
    /// <param name="sub"> 감소량 </param>
    private void HandleRottenItemCollision(int sub)
    {
        while (true)
        {
            // Debug.Log("목숨 - " + sub);
            Player.health -= sub;
            HealthUI.Inst.UpdateHeartsUI();
            // AudioManager.Instance.PlaySFX("Cat_Attack");
            // AudioManager.Instance.PlaySFX("Item_Kill");
            GameManager.Inst.CheckGameOver();
            break;
        }
    }

    /// <summary>
    /// 깃털, 쥐돌이, 생선 인형: 점수 증가
    /// </summary>
    /// <param name="score"></param>
    private void HandleScoreItemCollision(int score)
    {
        Debug.Log("점수 + " + score);
        ScoreManager.scoreCount += score;
        // AudioManager.Instance.PlaySFX("Item_Heal");
    }
}
