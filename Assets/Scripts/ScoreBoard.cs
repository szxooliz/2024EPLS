using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestScoreTextName;
    public TextMeshProUGUI secondScoreText;
    public TextMeshProUGUI secondScoreTextName;
    public TextMeshProUGUI thirdScoreText;
    public TextMeshProUGUI thirdScoreTextName;
    // public TextMeshProUGUI CoinText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        bestScoreText.text = PlayerPrefs.GetInt("HiScore") + "";
        bestScoreTextName.text = PlayerPrefs.GetString("BestScoreTime");
        secondScoreText.text = PlayerPrefs.GetInt("SecondScore") + "";
        secondScoreTextName.text = PlayerPrefs.GetString("SecondScoreTime");
        thirdScoreText.text = PlayerPrefs.GetInt("ThirdScore") + "";
        thirdScoreTextName.text = PlayerPrefs.GetString("ThirdScoreTime");
        //CoinText.text = "coin: " + PlayerPrefs.GetInt("Coin");
    }
}
