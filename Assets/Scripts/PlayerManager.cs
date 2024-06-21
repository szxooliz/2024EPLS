using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;
using System;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public Text scoreText1;
    private void Awake()
    {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            scoreText1.text = "Á¡¼ö :           " + ScoreManager.scoreCount;
            Time.timeScale = 0f;

            if (ScoreManager.scoreCount < TopScores.Instance.BestScore)
            {
                if (ScoreManager.scoreCount < TopScores.Instance.SecondScore)
                {
                    if (ScoreManager.scoreCount < TopScores.Instance.ThirdScore)
                        return;
                    TopScores.Instance.ThirdScore = ScoreManager.scoreCount;
                    TopScores.Instance.SaveDataByPlayerPrefs("ThirdScore", ScoreManager.scoreCount);
                    TopScores.Instance.ThirdScoreTime = DateTime.Now.ToString();
                    TopScores.Instance.SaveDataByPlayerPrefsString("ThirdScoreTime", TopScores.Instance.ThirdScoreTime);
                    return;
                }
                TopScores.Instance.ThirdScore = TopScores.Instance.SecondScore;
                TopScores.Instance.SaveDataByPlayerPrefs("ThirdScore", TopScores.Instance.SecondScore);
                TopScores.Instance.ThirdScoreTime = TopScores.Instance.SecondScoreTime;
                TopScores.Instance.SaveDataByPlayerPrefsString("ThirdScoreTime", TopScores.Instance.SecondScoreTime);

                TopScores.Instance.SecondScore = ScoreManager.scoreCount;
                TopScores.Instance.SaveDataByPlayerPrefs("SecondScore", ScoreManager.scoreCount);
                TopScores.Instance.SecondScoreTime = DateTime.Now.ToString();
                TopScores.Instance.SaveDataByPlayerPrefsString("SecondScoreTime", TopScores.Instance.SecondScoreTime);
                return;
            }
            if (ScoreManager.scoreCount == TopScores.Instance.BestScore) return;

            TopScores.Instance.ThirdScore = TopScores.Instance.SecondScore;
            TopScores.Instance.SaveDataByPlayerPrefs("ThirdScore", TopScores.Instance.SecondScore);
            TopScores.Instance.ThirdScoreTime = TopScores.Instance.SecondScoreTime;
            TopScores.Instance.SaveDataByPlayerPrefsString("ThirdScoreTime", TopScores.Instance.SecondScoreTime);

            TopScores.Instance.SecondScore = TopScores.Instance.BestScore;
            TopScores.Instance.SaveDataByPlayerPrefs("SecondScore", TopScores.Instance.BestScore);
            TopScores.Instance.SecondScoreTime = TopScores.Instance.BestScoreTime;
            TopScores.Instance.SaveDataByPlayerPrefsString("SecondScoreTime", TopScores.Instance.BestScoreTime);

            TopScores.Instance.BestScore = ScoreManager.scoreCount;
            TopScores.Instance.SaveDataByPlayerPrefs("BestScore", ScoreManager.scoreCount);
            TopScores.Instance.BestScoreTime = DateTime.Now.ToString();
            TopScores.Instance.SaveDataByPlayerPrefsString("BestScoreTime", TopScores.Instance.BestScoreTime);
        } 
    }
}