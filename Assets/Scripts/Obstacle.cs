using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 해당 장애물이 플레이어와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("Pipe"))
            {
                HandlePipeCollision();
            }
            else if (gameObject.CompareTag("Clover"))
            {
                HandleCloverCollision();
            }
        }
    }

    /// <summary>
    /// 스크립트가 부착된 장애물이 끈끈이주걱, 덩굴인 경우
    /// </summary>
    /// <param name="player"></param>
    private void HandlePipeCollision()
    {
        Player.health--;
        
        if (animator != null)
        {
            // 피격 애니메이션 발동
            animator.SetTrigger("Active");
        }

        HealthUI.Inst.UpdateHeartsUI();
        //AudioManager.Instance.PlaySFX("Cat_Attack");
        //AudioManager.Instance.PlaySFX("Item_Kill");
        GameManager.Inst.CheckGameOver();
    }

    /// <summary>
    /// 스크립트가 부착된 장애물이 클로버인 경우
    /// </summary>
    private void HandleCloverCollision()
    {

        if (animator != null)
        {
            // 피격 애니메이션 발동
            animator.SetTrigger("Active");
        }

        KnockBack.instance.TriggerKnockBack();
        // Debug.Log("Clover");
    }
}