using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public static int health = 3;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private void Awake()
    {
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

    // Update is called once per frame
    void Update()
    {
        foreach (Image img in hearts)
        {
            img.sprite = emptyHeart;
        }
        for (int i = 0; i < health; i++)
        {
            hearts[i].sprite = fullHeart;
        }
    }
}
