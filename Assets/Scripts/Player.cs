using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Inst;
    public static int health = 3; // 플레이어 체력
    public Animator animator; // 플레이어 애니메이터
    public static SpriteRenderer renderer;
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
        renderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator FlickerCharacter()
    {
        renderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        renderer.color = Color.white;
    }

    /// <summary>
    /// 특정 아이템과 충돌할 경우, 캐릭터를 깜빡입니다.
    /// </summary>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("LifeMinus")) { StartCoroutine(FlickerCharacter()); }
        if (collision.gameObject.CompareTag("LifeMinus-2")) { StartCoroutine(FlickerCharacter()); }
        if (collision.gameObject.CompareTag("Pipe")) { StartCoroutine(FlickerCharacter()); }
    }
}
