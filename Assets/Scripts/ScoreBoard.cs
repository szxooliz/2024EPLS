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
        BestScoreText.text = "1. " + PlayerPrefs.GetInt("HiScore") + "  " + PlayerPrefs.GetString("BestScoreTime");
        SecondScoreText.text = "1. " + PlayerPrefs.GetInt("SecondScore") + "  " + PlayerPrefs.GetString("SecondScoreTime");
        ThirdScoreText.text = "1. " + PlayerPrefs.GetInt("ThirdScore") + "  " + PlayerPrefs.GetString("ThirdScoreTime");
    }
}
