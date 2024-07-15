using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI[] text_highScores;
    public TextMeshProUGUI[] text_highScoreTimes;
    // public TextMeshProUGUI bestScoreText;
    // public TextMeshProUGUI bestScoreTextName;
    // public TextMeshProUGUI secondScoreText;
    // public TextMeshProUGUI secondScoreTextName;
    // public TextMeshProUGUI thirdScoreText;
    // public TextMeshProUGUI thirdScoreTextName;

    void Update()
    {
        // bestScoreText.text = PlayerPrefs.GetInt("HiScore") + "";
        // bestScoreTextName.text = PlayerPrefs.GetString("BestScoreTime");
        // secondScoreText.text = PlayerPrefs.GetInt("SecondScore") + "";
        // secondScoreTextName.text = PlayerPrefs.GetString("SecondScoreTime");
        // thirdScoreText.text = PlayerPrefs.GetInt("ThirdScore") + "";
        // thirdScoreTextName.text = PlayerPrefs.GetString("ThirdScoreTime");
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
