using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public static int scoreCount;
    private float scoreTimer;
    private const float scoreInterval = 1f; // 1�ʸ��� ���� ����

    void Start()
    {
        scoreCount = 0;
        scoreTimer = 0f;
        UpdateScoreText();
    }

    void Update()
    {
        scoreTimer += Time.deltaTime;

        if (scoreTimer >= scoreInterval)
        {
            scoreCount++;
            scoreTimer -= scoreInterval;
            UpdateScoreText();
        }
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + scoreCount;
    }
}