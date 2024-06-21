using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text BestScoreText;
    public Text SecondScoreText;
    public Text ThirdScoreText;
   
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BestScoreText.text = "1. " + TopScores.Instance.BestScore + "    " + TopScores.Instance.BestScoreTime;
        SecondScoreText.text = "2. " + TopScores.Instance.SecondScore + "    " + TopScores.Instance.SecondScoreTime;
        ThirdScoreText.text = "3. " + TopScores.Instance.ThirdScore + "    " + TopScores.Instance.ThirdScoreTime;
    }
}
