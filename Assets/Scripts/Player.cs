using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    public static Player Inst;
    public static int health = 3; // 플레이어 체력
    public Animator animator; // 플레이어 애니메이터
    public static SpriteRenderer playerRenderer;
    public float defaultY = -2.43f;
    void Awake()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(gameObject);
            Inst = this;
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        playerRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 특정 아이템과 충돌할 경우, 캐릭터를 깜빡입니다.
        if (collision.gameObject.CompareTag("LifeMinus")) { StartCoroutine(FlickerCharacter()); }
        if (collision.gameObject.CompareTag("LifeMinus-2")) { StartCoroutine(FlickerCharacter()); }
        if (collision.gameObject.CompareTag("Pipe")) { StartCoroutine(FlickerCharacter()); }
    }

    public IEnumerator FlickerCharacter()
    {
        playerRenderer.color = new Color(1, 0, 0, 0.7f);
        yield return new WaitForSeconds(0.1f);
        playerRenderer.color = Color.white;
    }

    /// <summary>
    /// 고양이 하강
    /// </summary>
    /// <returns></returns>
    public IEnumerator FallDown()
    {
        Debug.Log("고양이 내려가요~~");
        // 고양이 Y축 변경
        transform.DOMoveY(defaultY, 1f);
        yield return new WaitForSeconds(1f);

        animator.SetTrigger("Dead");
    }
}
