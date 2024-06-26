using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static int scoreCount;
    private float scoreTimer;
    [SerializeField] private const float scoreInterval = 1f; // 1�ʸ��� ���� ����
    private float previousScoreCount;

    void Start()
    {
        scoreCount = 0;
        scoreTimer = 0f;
        UpdateScoreText();
    }

    void Update()
    {
        scoreTimer += Time.deltaTime;
        PlayerPrefs.SetInt("Coin", CoinManager.coin);
        if (scoreTimer >= scoreInterval)
        {
            scoreCount++;
            scoreTimer -= scoreInterval;
        }

        if (scoreCount != previousScoreCount)
        {
            UpdateScoreText();
            previousScoreCount = scoreCount;
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreCount;
    }
}