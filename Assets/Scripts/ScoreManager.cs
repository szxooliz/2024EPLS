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
    public TextMeshProUGUI text_DisplayCoin;
    public static int scoreCount;
    private float scoreTimer;
    [SerializeField] private const float scoreInterval = 1f;
    // public static bool isSpringing = false;

    // 게임 오버 팝업 점수 표시 용도
    public GameObject popup_GameOver;
    public GameObject panel;
    public TextMeshProUGUI text_Score;
    public TextMeshProUGUI text_Coin;

    // 게임 오버 팝업 나타냄 효과 용도
    public GameObject[] panelObjects;
    [SerializeField] private float startDelay = 0.8f;
    [SerializeField] private float delay = 0.3f;

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
    }

    void Update()
    {
        if (!GameManager.Inst.isGameOver)
        {
            PlayerPrefs.SetInt("Coin", CoinManager.coin);

            if (!Clover.isCloverTriggered)
            {
                scoreTimer += Time.deltaTime;
            }

            if (scoreTimer >= scoreInterval)
            {
                scoreCount++;
                scoreTimer -= scoreInterval;
                UpdateScoreText();
            }
        }
    }

    /// <summary>
    /// 게임 내 점수 UI 표시
    /// </summary>
    private void UpdateScoreText()
    {
        text_DisplayScore.text = scoreCount.ToString();
    }

    /// <summary>
    /// 게임 내 코인 UI 표시
    /// </summary>
    public void UpdateCoinText()
    {
        text_DisplayCoin.text = CoinManager.Inst.playCoin.ToString();
    }
    
    /// <summary>
    /// 게임 종료 시 게임 오버 팝업 등장 및 세팅
    /// </summary>
    public void DisplayPopupGameOver()
    {
        popup_GameOver.SetActive(true);
        panel.SetActive(true);

        text_Score.text = "점수 : " + scoreCount;
        text_Coin.text = "코인 : " + CoinManager.Inst.playCoin;

        foreach (GameObject panelObject in panelObjects)
        {
            panelObject.SetActive(false);
        }

        StartCoroutine(StartWithDelay(startDelay));
    }

    /// <summary>
    /// GameOverSequentialActive() 시작 지연 코루틴
    /// </summary>
    /// <param name="startDelay"></param>
    /// <returns></returns>
    IEnumerator StartWithDelay(float startDelay)
    {
        // 지연 시간만큼 대기
        yield return new WaitForSecondsRealtime(startDelay);

        StartCoroutine(GameOverSequentialActive());
    }

    /// <summary>
    /// 게임 오버 팝업 위 오브젝트 순차적 활성화 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator GameOverSequentialActive()
    {
        foreach(GameObject panelObject in panelObjects)
        {
            panelObject.SetActive(true);
            yield return new WaitForSecondsRealtime(delay);
        }
    }

    /// <summary>
    /// 새로운 점수를 추가하고 정렬
    /// </summary>
    /// <param name="score"> 점수 </param>
    /// <param name="time"> 점수가 기록된 시간 </param>
    public void AddNewScore(int score, string time)
    {
        // 이미 같은 점수가 존재하는지 확인
        if (!highScores.Exists(record => record.score == score))
        {
            highScores.Add(new Record(score, time));
            highScores.Sort((x, y) => y.score.CompareTo(x.score));

            // 최고 기록 3개까지만 유지
            if (highScores.Count > 3)
            {
                highScores.RemoveAt(3);
            }
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