using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN : MonoBehaviour
{
    public void BTN_Replay()
    {
        GameManager.Inst.OnClickReplay();
        GameManager.Inst.isGameOver = false;
        ScoreManager.scoreCount = 0;
        CoinManager.Inst.playCoin = 0;
    }

    public void BTN_Main()
    {
        GameManager.Inst.isGameOver = false;
        ScoreManager.scoreCount = 0;
        CoinManager.Inst.playCoin = 0;
        GameManager.Inst.OnClickBack();
    }
}
