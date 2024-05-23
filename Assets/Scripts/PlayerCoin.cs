using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCoin : MonoBehaviour
{
    // 플레이어가 보유하고 있는 코인
    public int playerCoin;

    /// <summary>
    /// 상점에서 스킨 구매 시 코인 삭감
    /// </summary>
    /// <param name="coinToRemove"></param>
    /// <returns></returns>
    public bool TryRemoveCoin(int coinToRemove)
    {
        if (playerCoin >= coinToRemove)
        {
            playerCoin -= coinToRemove;
            return true;
        }
        else
        {
            return false;
        }
    }
}
