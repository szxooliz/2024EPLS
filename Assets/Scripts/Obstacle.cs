using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 해당 장애물이 플레이어와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Pipe"))
            {
                HandlePipeCollision(collision.gameObject);
            }
            else if (gameObject.CompareTag("Clover"))
            {
                HandleCloverCollision();
            }
        }
    }

    /// <summary>
    /// 스크립트가 부착된 장애물이 파이프, 덩굴인 경우
    /// </summary>
    /// <param name="player"></param>
    private void HandlePipeCollision(GameObject player)
    {
        HealthManager.health--;
        AudioManager.Instance.PlaySFX("Cat_Attack");
        AudioManager.Instance.PlaySFX("Item_Kill");

        GameManager.Inst.CheckGameOver(player);
    }

    /// <summary>
    /// 스크립트가 부착된 장애물이 클로버인 경우
    /// </summary>
    private void HandleCloverCollision()
    {
        StartCoroutine(KnockBack.instance.KnockBackCoroutine());
        Debug.Log("Clover");
    }
}