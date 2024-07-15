using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    // public static ScoreManager _score;
    // public static HealthManager _health;
    // public static SkinManager _skin;
    // public static PopupManager _popup;
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
        SceneManager.LoadScene("PlayScene 3");
        AudioManager.Instance.PlaySFX("Button_UI");
    }

    public void OnClickCostumeRoom()
    {
        SceneManager.LoadScene("CostumeScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }
    public void OnClickBack()
    {
        SceneManager.LoadScene("StartScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }

    public void OnClickReplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene 3");
        AudioManager.Instance.PlaySFX("Button_UI");
    }
    // -------------------------------------------

    /// <summary>
    /// 목숨 0이면 게임 오버
    /// </summary>
    /// <param name="player"></param>
    public void CheckGameOver()
    {
        Debug.Log("GameManager CheckGameOver ___ 남은 목숨 : " + HealthManager.health);
        // 체력 0 이하 되어 게임 오버 되었을 때
        if (HealthManager.health <= 0)
        {
            // PlayerManager.isGameOver = true;
            // player.SetActive(false);
            ScoreManager.Inst.DisplayPopupGameOver();
            ScoreManager.Inst.AddNewScore(ScoreManager.scoreCount, DateTime.Now.ToString());
            ScoreManager.Inst.SaveHighScores();

            // Time.timeScale = 0f;
        }
        else
        {
            return;
        }
    }
}
