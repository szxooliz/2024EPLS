using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 코인의 전체 관리 담당
/// </summary>
public class CoinManager : MonoBehaviour
{
    public static CoinManager Inst; // Singleton
    public static int coin = 0; // 플레이어가 보유하고 있는 코인
    public int playCoin = 0; // 게임 플레이 내에서 획득한 코인

    void Awake() 
    {
        // Singleton
        if(Inst == null) 
        {
            Inst = this; 
            // DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        Debug.Log("플레이 내 코인 : " + playCoin);

        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            PlayerPrefs.SetInt("Coin", coin);
        }
    }

    void Update()
    {
        PlayerPrefs.GetInt("Coin", coin);
    }

    /// <summary>
    /// 코인 추가
    /// </summary>
    /// <param name="amount"></param>
    public void AddCoin(int amount)
    {
        coin += amount;
        PlayerPrefs.SetInt("Coin", coin);
        // CoinUIManager.Inst.UpdateCoinUI();
    }

    /// <summary>
    /// 상점에서 스킨 구매 시 코인 차감
    /// </summary>
    /// <param name="coinToRemove"></param>
    /// <returns></returns>
    public static bool TryRemoveCoin(int coinToRemove)
    {
        if (PlayerPrefs.GetInt("Coin") >= coinToRemove)
        {
            coin -= coinToRemove;
            PlayerPrefs.SetInt("Coin", coin);

            // 코인 차감 시 UI 업데이트
            CoinUIManager.Inst.UpdateCoinUI();
            return true;
        }
        else
        {
            return false;
        }
    }
}
