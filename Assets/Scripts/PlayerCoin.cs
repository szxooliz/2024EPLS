using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerCoin
{
    // 플레이어가 보유하고 있는 코인
    public static int playerCoin;

    /// <summary>
    /// 상점에서 스킨 구매 시 코인 삭감
    /// </summary>
    /// <param name="coinToRemove"></param>
    /// <returns></returns>
    public static bool TryRemoveCoin(int coinToRemove)
    {
        if (playerCoin >= coinToRemove)
        {
            playerCoin -= coinToRemove;
            return true;
        }
        else
        {
            // 코인 부족 알림 팝업 뜨도록
            return false;
        }
    }
}
