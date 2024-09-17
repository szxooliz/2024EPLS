using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Animator animator;

    private const string STICKY = "Sticky";
    private const string DUNGUL = "Dungul";


    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 해당 장애물이 플레이어와 충돌 시
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag(STICKY))
            {
                Debug.Log("STICKY");
                HandleStickyCollision();
            }
            else if (gameObject.CompareTag(DUNGUL))
            {
                Debug.Log("DUNGUL");
                HandleObstacleCollision();
            }
        }
    }
    /// <summary>
    /// 스크립트가 부착된 장애물이 덩굴인 경우
    /// </summary>
    /// <param name="player"></param>

    private void HandleObstacleCollision()
    {
        Player.health--;

        if (animator != null)
        {
            // 피격 애니메이션 발동
            animator.SetTrigger("Active");
        }

        HealthUI.Inst.UpdateHeartsUI();

        Debug.Log("HandlePipeCollision ___ CheckGameOver");
        GameManager.Inst.CheckGameOver();

        AudioManager.Instance.PlaySFX("Cat_Attack");
    }

    /// <summary>
    /// 스크립트가 부착된 장애물이 끈끈이 주걱인 경우
    /// </summary>
    /// <param name="player"></param>
    private void HandleStickyCollision()
    {
        HandleObstacleCollision();
        AudioManager.Instance.PlaySFX("OB_ggeunggeunii");
    }




}