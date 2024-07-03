using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

/// <summary>
/// 코인의 전체 관리 담당
/// </summary>
public class CoinManager : MonoBehaviour
{
    public static CoinManager Inst; //Singleton
    public static int coin = 0;

    void Awake() 
    {
        // Singleton
        if(Inst == null) 
        {
            Inst = this; 
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
        else
        {
            PlayerPrefs.SetInt("Coin", coin);
        }
        
        CoinUIManager.Inst.UpdateCoinUI(); // 초기 UI 업데이트
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
        CoinUIManager.Inst.UpdateCoinUI();
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
