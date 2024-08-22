using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    public bool isGameOver = false;

    void Awake()
    {
        // Singleton
        if(Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(Inst);
        }

        // 테스트용 코드
        // #if UNITY_EDITOR
        // PlayerPrefs.DeleteAll();
        // #endif
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
        isGameOver = false;
        //AudioManager.Instance.PlaySFX("Button_UI");
    }
    // -------------------------------------------

    /// <summary>
    /// 목숨 0이면 게임 오버
    /// </summary>
    public void CheckGameOver()
    {
        Debug.Log("CheckGameOver");
        // 체력 0 이하 되어 게임 오버 되었을 때
        if (Player.health <= 0)
        {
            // Die 애니메이션 발동
            Player.Inst.animator.SetTrigger("Dead");
            isGameOver = true;

            // Move.instance.PauseMovement();
            BackGroundLoop.instance.PauseMovement();

            // 새로운 점수 저장하고 최고 기록 정렬
            ScoreManager.Inst.AddNewScore(ScoreManager.scoreCount, DateTime.Now.ToString());
            ScoreManager.Inst.SaveHighScores();

            Invoke("InvokeDisplayPopupGameOver", 0.5f);
        }
        else
        {
            return;
        }
    }

    private void InvokeDisplayPopupGameOver()
    {
        ScoreManager.Inst.DisplayPopupGameOver();
    }
}
