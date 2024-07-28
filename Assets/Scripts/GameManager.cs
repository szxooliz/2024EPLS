using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;

    void Awake()
    {
        // Singleton
        if(Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
        }
    }

    // ------------ Scene 전환 함수들 ------------
    public void OnClickGameStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_PlayScene");
        //AudioManager.Instance.PlaySFX("Button_UI");
    }

    public void OnClickCostumeRoom()
    {
        SceneManager.LoadScene("_CostumeScene");
        //AudioManager.Instance.PlaySFX("Button_UI");
    }
    public void OnClickBack()
    {
        SceneManager.LoadScene("_StartScene");
        //AudioManager.Instance.PlaySFX("Button_UI");
    }

    public void OnClickReplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("_PlayScene");
        //AudioManager.Instance.PlaySFX("Button_UI");
    }
    // -------------------------------------------

    /// <summary>
    /// 목숨 0이면 게임 오버
    /// </summary>
    public void CheckGameOver()
    {
        // 체력 0 이하 되어 게임 오버 되었을 때
        if (Player.health <= 0)
        {
            // Die 애니메이션 발동
            Player.Inst.animator.SetTrigger("Dead");

            // 새로운 점수 저장하고 최고 기록 정렬
            ScoreManager.Inst.AddNewScore(ScoreManager.scoreCount, DateTime.Now.ToString());
            ScoreManager.Inst.SaveHighScores();

            // Die 애니메이션 안나와서 invoke로 노선 틀음
            // ScoreManager.Inst.DisplayPopupGameOver();

            Invoke("InvokeDisplayPopupGameOver", 0.5f);
            // Invoke("InvokeGameStop", 1.0f);
        }
        else
        {
            return;
        }
    }
    private void InvokeGameStop()
    {
        Time.timeScale = 0f;
    }
    private void InvokeDisplayPopupGameOver()
    {
        ScoreManager.Inst.DisplayPopupGameOver();
    }
}
