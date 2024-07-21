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
            InitializeUIReferences();
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

    /// <summary>
    /// UI 참조 초기화
    /// </summary>
    void InitializeUIReferences()
    {
        if (playerCoinText == null)
        {
            playerCoinText = GameObject.Find("TXT_Coin").GetComponent<TextMeshProUGUI>();
        }
    }

    /// <summary>
    /// 코인 업데이트
    /// </summary>
    public void UpdateCoinUI()
    {
        if (playerCoinText != null)
        {
            playerCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
        }
    }
}
