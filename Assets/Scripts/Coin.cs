using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coin = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.GetInt("Coin", coin);
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
            return true;
        }
        else
        {
            return false;
        }
    }
}
