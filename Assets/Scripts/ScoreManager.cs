using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Inst;
    // 플레이 중 점수 표시 용도
    public TextMeshProUGUI text_DisplayScore;
    public static int scoreCount;
    private float scoreTimer;
    [SerializeField] private const float scoreInterval = 1f; // 1�ʸ��� ���� ����

    // 게임 오버 팝업 점수 표시 용도
    public GameObject popup_GameOver;
    public GameObject panel;
    public TextMeshProUGUI text_Score;
    public TextMeshProUGUI text_Coin;
    private GameObject GameOverList;

    /* 최고 기록 데이터 관리 용도 << 사용 안 할 예정
    // public static int hiScoreCount = 0;
    // public static int secondScoreCount = 0;
    // public static int thirdScoreCount = 0;

    // public static string bestScoreTime = "00";
    // public static string secondScoreTime = "00";
    // public static string thirdScoreTime = "00";
    */

    // 기록 저장, 정렬용 리스트 
    private List<Record> highScores = new List<Record>(); 

    private void Awake()
    {
        Inst = this;
    }
    void Start()
    {
        scoreCount = 0;
        scoreTimer = 0f;
        UpdateScoreText();

        LoadHighScores();
        GameOverList = popup_GameOver.transform.Find("PopUpList").gameObject;
    }

    void Update()
    {
        scoreTimer += Time.deltaTime;
        PlayerPrefs.SetInt("Coin", CoinManager.coin);
        
        if (scoreTimer >= scoreInterval)
        {
            scoreCount++;
            scoreTimer -= scoreInterval;
            UpdateScoreText(); // << 이 조건문 안에 넣어버리면 previousScoreCount 필요 없을 듯
        }
    }

    /// <summary>
    /// 게임 내 점수 UI 표시
    /// </summary>
    private void UpdateScoreText()
    {
        text_DisplayScore.text = "점수 : " + scoreCount;
    }
    
    /// <summary>
    /// 게임 종료 시 게임 오버 팝업 등장 및 세팅
    /// </summary>
    public void DisplayPopupGameOver()
    {
        Time.timeScale = 0f;
        popup_GameOver.SetActive(true);
        panel.SetActive(true);

        text_Score.text = "점수 : " + scoreCount;
        text_Coin.text = "코인 : " + CoinManager.Inst.playCoin;

        //PartOfGameOver();
    }

    /// <summary>
    /// 게임 종료 시 0.5초 뒤 팝업창의 일부분 등장
    /// </summary>
    /* public void PartOfGameOver()
    {  
        // 먼저 TXT_RecordList를 비활성화
        GameOverList.SetActive(false);

        // 1초 후에 ActivateRecordList 메서드 호출
        Invoke("ActivateRecordList", 0.5f);
    }

    private void ActivateRecordList()
    {
        // 특정 게임 오브젝트를 활성화
        SetActiveRecursively(GameOverList, true);
    }
    private void SetActiveRecursively(GameObject obj, bool state)
    {
        obj.SetActive(state);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, state);
        }
    } */
    /// <summary>
    /// 새로운 점수를 추가하고 정렬
    /// </summary>
    /// <param name="score"> 점수 </param>
    /// <param name="time"> 점수가 기록된 시간 </param>
    public void AddNewScore(int score, string time)
    {
        highScores.Add(new Record(score, time));
        highScores.Sort((x, y) => y.score.CompareTo(x.score));

        if (highScores.Count > 3)
        {
            // 최고 기록 3개까지만 유지
            highScores.RemoveAt(3);
        }
    }

    /// <summary>
    /// 이전에 저장된 최고 기록을 불러옴
    /// </summary>
    public void LoadHighScores()
    {
        // 기존 리스트 초기화 
        highScores.Clear();

        for (int i = 0; i < 3; i++)
        {
            if (PlayerPrefs.HasKey("HighScore" + i))
            {
                int score = PlayerPrefs.GetInt("HighScore" + i);
                string time= PlayerPrefs.GetString("HighScoreTime" + i);
                highScores.Add(new Record(score, time));
            }
        }
    }

    /// <summary>
    /// 현재 리스트에 저장된 점수와 시간을 PlayerPrefs에 저장
    /// </summary>
    public void SaveHighScores()
    {
        for (int i = 0; i < highScores.Count; i++)
        {
            PlayerPrefs.SetInt("HighScore" + i, highScores[i].score);
            PlayerPrefs.SetString("HighScoreTime" + i, highScores[i].time);
        }

        // 변경 사항 저장
        PlayerPrefs.Save();
    }

    /// <summary>
    /// 기록, 시간 짝지어 저장하는 객체
    /// </summary>
    [Serializable]
    private class Record
    {
        public int score;
        public string time;

        public Record(int score, string time)
        {
            this.score = score;
            this.time = time;
        }
    }
}