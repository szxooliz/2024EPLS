using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI secondScoreText;
    public TextMeshProUGUI thirdScoreText;
    // public TextMeshProUGUI CoinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestScoreText.text = "1. " + PlayerPrefs.GetInt("HiScore") + "  " + PlayerPrefs.GetString("BestScoreTime");
        secondScoreText.text = "2. " + PlayerPrefs.GetInt("SecondScore") + "  " + PlayerPrefs.GetString("SecondScoreTime");
        thirdScoreText.text = "3. " + PlayerPrefs.GetInt("ThirdScore") + "  " + PlayerPrefs.GetString("ThirdScoreTime");
        //CoinText.text = "coin: " + PlayerPrefs.GetInt("Coin");
    }
}
