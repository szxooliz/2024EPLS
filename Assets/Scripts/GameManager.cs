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
        //AudioManager.Instance.PlaySFX("Button_UI");
    }
    // -------------------------------------------

    /// <summary>
    /// 목숨 0이면 게임 오버
    /// </summary>
    public void CheckGameOver()
    {
        Debug.Log(" 남은 체력 : " + Player.health);

        // 체력 0 이하 되어 게임 오버 되었을 때
        if (Player.health <= 0)
        {
            Debug.Log("CheckGameOver___게임 오버요");
            SetGameOver();
        }
        else
        {
            Debug.Log("CheckGameOver___아직 게임오버 아님");
            return;
        }
    }
    /// <summary>
    /// 게임 오버 효과
    /// </summary>
    public void SetGameOver()
    {
        isGameOver = true;
        BackGroundLoop.instance.PauseMovement();

        // 땅에 떨어진 후 죽은 애니메이션은 코루틴으로 변경
        // 여기는 코루틴 자리

        // Die 애니메이션 발동 - 나중에 코루틴 안으로 넣기
        Player.Inst.animator.SetTrigger("Dead");

        // 새로운 점수 저장하고 최고 기록 정렬
        ScoreManager.Inst.AddNewScore(ScoreManager.scoreCount, DateTime.Now.ToString());
        ScoreManager.Inst.SaveHighScores();

        Debug.Log("SetGameOver ___ 게임 오버 팝업 내려옴...");
        Invoke("InvokeDisplayPopupGameOver", 1.0f);
    }

    private void InvokeDisplayPopupGameOver()
    {
        ScoreManager.Inst.DisplayPopupGameOver();
    }
}
