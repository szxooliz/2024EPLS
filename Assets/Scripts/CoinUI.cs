using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinUI : MonoBehaviour
{
    public TextMeshProUGUI playerCoin;
    // Start is called before the first frame update
    void Start()
    {
        playerCoin.text = Coin.coin.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        // 코인 차감 시에만 업데이트

    }
}
