using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 코인 오브젝트의 충돌 처리 담당
/// </summary>
public class Coin : MonoBehaviour
{
    private const string COIN_SMALL = "CoinSmall";
    private const string COIN_BIG = "CoinBig";

    [SerializeField] private int amount;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인이 플레이어와 충돌 시 획득
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag(COIN_SMALL))
            {
                amount = 5;
            }
            else if (gameObject.CompareTag(COIN_BIG))
            {
                amount = 10;
            }
            
            CoinManager.Inst.playCoin += amount;
            CoinManager.Inst.AddCoin(amount);
            ScoreManager.Inst.UpdateCoinText();
            // AudioManager.Instance.PlaySFX("Item_Heal");

            Destroy(gameObject);
        }
    }
}
