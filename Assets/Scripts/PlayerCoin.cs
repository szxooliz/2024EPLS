using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCoin
{
    /// <summary>
    /// 상점에서 스킨 구매 시 코인 삭감
    /// </summary>
    /// <param name="coinToRemove"></param>
    /// <returns></returns>
    public static bool TryRemoveCoin(int coinToRemove)
    {
        if (PlayerPrefs.GetInt("Coin") >= coinToRemove)
        {
            Coin.coin -= coinToRemove;
            PlayerPrefs.SetInt("Coin", Coin.coin);
            return true;
        }
        else
        {
            return false;
        }
    }
}
