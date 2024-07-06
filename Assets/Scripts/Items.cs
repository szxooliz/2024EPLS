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

    private void Start()
    {
        //AudioManager.Instance.PlayMusic("Cat_Walk");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //StartCoroutine(PauseMusicCoroutine());

        // 해당 아이템이 플레이어와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag(ITEM_TUNA))
            {
                HandleTunaCollision();
            }
            else if (gameObject.CompareTag(ITEM_ROTTEN_FISH))
            {
                HandleRottenItemCollision(collision.gameObject, 1);
            }
            else if (gameObject.CompareTag(ITEM_ROTTEN_TUNA))
            {
                HandleRottenItemCollision(collision.gameObject, 2);
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
        Debug.Log("목숨+1");
        if (HealthManager.health < 3)
        {
            HealthManager.health++;
            //AudioManager.Instance.PlaySFX("Item_Heal");
        }
    }

    /// <summary>
    /// 썩은 생선, 참치캔: 목숨 감소
    /// </summary>
    /// <param name="life"></param>
    private void HandleRottenItemCollision(GameObject player, int life)
    {
        Debug.Log("목숨 - " + life);
        HealthManager.health -= life;
        //AudioManager.Instance.PlaySFX("Cat_Attack");
        //AudioManager.Instance.PlaySFX("Item_Kill");

        GameManager.Inst.CheckGameOver(player);
    }

    /// <summary>
    /// 깃털, 쥐돌이, 생선 인형: 점수 증가
    /// </summary>
    /// <param name="score"></param>
    private void HandleScoreItemCollision(int score)
    {
        Debug.Log("점수 + " + score);
        ScoreManager.scoreCount += score;
        //AudioManager.Instance.PlaySFX("Item_Heal");
    }
    
    //private IEnumerator PauseMusicCoroutine()
    //{
    //    AudioManager.Instance.PauseMusic("Cat_Walk");
    //    yield return new WaitForSeconds(0.5f); // 예시로 0.5초 동안 일시 정지 상태 유지
    //    AudioManager.Instance.ResumeMusic("Cat_Walk"); // 일시 정지 해제
    //}
}
