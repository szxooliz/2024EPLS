using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI playerCoinText;
    // Start is called before the first frame update
    void Start()
    {
        UpdateCoinUI();
    }

    // Update is called once per frame
    void UpdateCoinUI()
    {
        // 코인 차감 시에만 업데이트
        playerCoinText.text = PlayerPrefs.GetInt("Coin").ToString();
    }
}
