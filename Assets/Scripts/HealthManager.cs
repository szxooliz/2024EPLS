using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static HealthManager Inst;
    public static int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        Inst = this;
        health = 3;
    }
    private void Start()
    {
        health = 3;

        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }

    /// <summary>
    /// 목숨 하트 UI 변경
    /// </summary>
    public void UpdateHeartsUI()
    {
        Debug.Log("HealthManager UpdateHeartsUI ___ 남은 목숨 : " + health);

        Debug.Log("----------- HealthManager UpdateHeartsUI foreach 시작 -----------");
        foreach (Image img in hearts)
        {
            Debug.Log("빈 하트로 스프라이트");
            img.sprite = emptyHeart;
        }
        Debug.Log("------------------------------------------------------------------");
        
        Debug.Log("------------- HealthManager UpdateHeartsUI for 시작 -------------");
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
            Debug.Log("하트 채우기 i : " + i );
        }
        Debug.Log("------------------------------------------------------------------");

    }
}
