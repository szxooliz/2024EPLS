using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// 코인 UI 업데이트
/// </summary>
public class CoinUIManager : MonoBehaviour
{
    public static CoinUIManager Inst;
    public TextMeshProUGUI playerCoinText;
    void Awake() 
    {
        // Singleton
        if (Inst == null)
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
        UpdateCoinUI();
    }

    // 코인 업데이트
    public void UpdateCoinUI()
    {
        playerCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
}
