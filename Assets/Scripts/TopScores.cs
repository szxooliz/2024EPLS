using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TopScores : MonoBehaviour
{
    private static TopScores instance = null;
    public static TopScores Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("TopScores").AddComponent<TopScores>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        GameObject go = GameObject.Find("TopScores");
        if (go == null)
        {
            go = new GameObject { name = "TopScores" };
            go.AddComponent<TopScores>();
        }
        DontDestroyOnLoad(go);
        instance = go.GetComponent<TopScores>();
    }
    private int bestScore = 0;
    private int secondScore = 0;
    private int thirdScore = 0;
    private string bestScoreTime = "00";
    private string secondScoreTime = "00";
    private string thirdScoreTime = "00";
    public int BestScore { get { return bestScore; } set { bestScore = value; } }
    public int SecondScore { get { return secondScore; } set { secondScore = value; } }
    public int ThirdScore {  get { return thirdScore; } set { thirdScore = value; } }
    public string BestScoreTime { get { return bestScoreTime; } set { bestScoreTime = value; } }
    public string SecondScoreTime { get { return secondScoreTime; } set { secondScoreTime = value; } }
    public string ThirdScoreTime { get { return thirdScoreTime; } set { thirdScoreTime = value; } }
    void Start()
    {
        BestScore = LoadDataByPlayerPrefs("BestScore");
        SecondScore = LoadDataByPlayerPrefs("SecondScore");
        ThirdScore = LoadDataByPlayerPrefs("ThridScore");
        BestScoreTime = LoadDataByPlayerPrefsString("BestScoreTime");
        SecondScoreTime = LoadDataByPlayerPrefsString("SecondScoreTime");
        ThirdScoreTime = LoadDataByPlayerPrefsString("ThirdScoreTime");
    }

    public void SaveDataByPlayerPrefs(string key, int value)
    {
        PlayerPrefs.SetInt(key, value);
        PlayerPrefs.Save();
    }
    public void SaveDataByPlayerPrefsString(string key, string value)
    {
        PlayerPrefs.SetString(key, value);
        PlayerPrefs.Save();
    }

    public int LoadDataByPlayerPrefs(string key)
    {
        return PlayerPrefs.GetInt(key);
    }
    public string LoadDataByPlayerPrefsString(string key)
    {
        return PlayerPrefs.GetString(key);
    }
}
