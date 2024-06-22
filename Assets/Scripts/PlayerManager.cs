//PlayerManager

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
    public static int hiScoreCount = 0;
    public static int secondScoreCount = 0;
    public static int thirdScoreCount = 0;

    public static  string bestScoreTime = "00";
    public static string secondScoreTime = "00";
    public static string thirdScoreTime = "00";
    private void Awake()
    {
        isGameOver = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("HighScore"))
        {
            hiScoreCount = PlayerPrefs.GetInt("HiScore");
            secondScoreCount = PlayerPrefs.GetInt("SecondScore");
            thirdScoreCount = PlayerPrefs.GetInt("ThirdScore");

            bestScoreTime = PlayerPrefs.GetString("BestScoreTime");
            secondScoreTime = PlayerPrefs.GetString("SecondScoreTime");
            thirdScoreTime = PlayerPrefs.GetString("ThirdScoreTime");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            scoreText1.text = "Á¡¼ö :           " + ScoreManager.scoreCount;

            if(ScoreManager.scoreCount > PlayerPrefs.GetInt("HiScore"))
            {
                thirdScoreCount = secondScoreCount;
                PlayerPrefs.SetInt("ThirdScore", thirdScoreCount);
                thirdScoreTime = secondScoreTime;
                PlayerPrefs.SetString("ThirdScoreTime", thirdScoreTime);

                secondScoreCount = hiScoreCount;
                secondScoreTime = bestScoreTime;
                PlayerPrefs.SetInt("SecondScore", secondScoreCount);
                PlayerPrefs.SetString("SecondScoreTime", bestScoreTime);

                hiScoreCount = ScoreManager.scoreCount;
                bestScoreTime = DateTime.Now.ToString();
                PlayerPrefs.SetInt("HiScore", hiScoreCount);
                PlayerPrefs.SetString("BestScoreTime", bestScoreTime);
            }
            if(ScoreManager.scoreCount < PlayerPrefs.GetInt("HiScore") && ScoreManager.scoreCount > PlayerPrefs.GetInt("SecondScore"))
            {
                thirdScoreCount = secondScoreCount;
                thirdScoreTime = secondScoreTime;
                PlayerPrefs.SetInt("ThirdScore", thirdScoreCount);
                PlayerPrefs.SetString("ThirdScoreTime", secondScoreTime);

                secondScoreCount = ScoreManager.scoreCount;
                secondScoreTime = DateTime.Now.ToString();
                PlayerPrefs.SetInt("SecondScore", secondScoreCount);
                PlayerPrefs.SetString("SecondScoreTime", secondScoreTime);
            }
            else if(ScoreManager.scoreCount < PlayerPrefs.GetInt("SecondScore") && ScoreManager.scoreCount > PlayerPrefs.GetInt("ThirdScore"))
            {
                thirdScoreCount = ScoreManager.scoreCount;
                thirdScoreTime = DateTime.Now.ToString();
                PlayerPrefs.SetInt("ThirdScore", thirdScoreCount);
                PlayerPrefs.SetString("ThirdScoreTime", thirdScoreTime);
            }
            Time.timeScale = 0f;
        }
    }
}