using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
    public GameObject gameOverScreen;
    public Text scoreText1;
    public Text BestScoreText;
    public Text SecondScoreText;
    public Text ThirdScoreText;
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
            scoreText1.text = "점수 :           " + ScoreManager.scoreCount;
            BestScoreText.text = "최고 점수 :           " + TopScores.Instance.BestScore;
            SecondScoreText.text = "2번째 점수 :           " + TopScores.Instance.SecondScore;
            ThirdScoreText.text = "3번째 점수 :           " + TopScores.Instance.ThirdScore;

            if (ScoreManager.scoreCount < TopScores.Instance.BestScore)
            {
                if(ScoreManager.scoreCount < TopScores.Instance.SecondScore)
                {
                    if (ScoreManager.scoreCount < TopScores.Instance.ThirdScore)
                        return;
                    TopScores.Instance.ThirdScore = ScoreManager.scoreCount;
                    TopScores.Instance.SaveDataByPlayerPrefs("ThirdScore", ScoreManager.scoreCount);
                    return;
                }
                TopScores.Instance.ThirdScore = TopScores.Instance.SecondScore;
                TopScores.Instance.SaveDataByPlayerPrefs("ThirdScore", TopScores.Instance.SecondScore);
                TopScores.Instance.SecondScore = ScoreManager.scoreCount;
                TopScores.Instance.SaveDataByPlayerPrefs("SecondScore", ScoreManager.scoreCount);
                return;
            }
            if (ScoreManager.scoreCount == TopScores.Instance.BestScore) return;
            TopScores.Instance.ThirdScore = TopScores.Instance.SecondScore;
            TopScores.Instance.SaveDataByPlayerPrefs("ThirdScore", TopScores.Instance.SecondScore);
            TopScores.Instance.SecondScore = TopScores.Instance.BestScore;
            TopScores.Instance.SaveDataByPlayerPrefs("SecondScore", TopScores.Instance.BestScore);
            TopScores.Instance.BestScore = ScoreManager.scoreCount;
            TopScores.Instance.SaveDataByPlayerPrefs("BestScore", ScoreManager.scoreCount);

            Time.timeScale = 0f;
        } 
    }
}