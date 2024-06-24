using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int coin = 0;

    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.HasKey("Coin"))
        {
            coin = PlayerPrefs.GetInt("Coin");
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.GetInt("Coin", coin);
    }
}
