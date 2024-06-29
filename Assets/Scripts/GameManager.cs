using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 목숨 0이면 게임 오버
    /// </summary>
    /// <param name="player"></param>
    public void CheckGameOver(GameObject player)
    {
        if (HealthManager.health <= 0)
        {
            PlayerManager.isGameOver = true;
            player.SetActive(false);
        }
    }
}
