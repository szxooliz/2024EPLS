using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public Text BestScoreText;
    public Text SecondScoreText;
    public Text ThirdScoreText;
    public Text CoinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BestScoreText.text = "1. " + PlayerPrefs.GetInt("HiScore") + "  " + PlayerPrefs.GetString("BestScoreTime");
        SecondScoreText.text = "2.. " + PlayerPrefs.GetInt("SecondScore") + "  " + PlayerPrefs.GetString("SecondScoreTime");
        ThirdScoreText.text = "3. " + PlayerPrefs.GetInt("ThirdScore") + "  " + PlayerPrefs.GetString("ThirdScoreTime");
        CoinText.text = "coin: " + PlayerPrefs.GetInt("Coin");
    }
}
