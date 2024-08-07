using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public static HealthUI Inst;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
        Inst = this;
        Player.health = 3;
    }
    private void Start()
    {
        Player.health = 3;

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
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }

        for (int i = 0; i < Player.health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
