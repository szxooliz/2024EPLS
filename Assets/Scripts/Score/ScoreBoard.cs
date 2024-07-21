using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreBoard : MonoBehaviour
{
    public Button btn_BestScore;
    public TextMeshProUGUI[] text_highScores;
    public TextMeshProUGUI[] text_highScoreTimes;
    private void Awake() 
    {
        btn_BestScore.onClick.AddListener(() => DisplayHighScore());
    }

    /// <summary>
    /// 최고 기록 팝업 내용 표시
    /// </summary>
    public void DisplayHighScore()
    {
        for(int i = 0; i < 3; i++)
        {
            text_highScores[i].text = PlayerPrefs.GetInt("HighScore" + i).ToString();
            text_highScoreTimes[i].text = PlayerPrefs.GetString("HighScoreTime" + i);
        }
    }
}
