using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 코인 오브젝트의 충돌 처리 담당
/// </summary>
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 코인이 플레이어와 충돌 시 획득
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("코인 +1");
            
            CoinManager.Inst.AddCoin(1);
            AudioManager.Instance.PlaySFX("Item_Heal");

            Destroy(gameObject);
        }
    }
}
