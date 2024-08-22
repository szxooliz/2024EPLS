using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Inst;
    public static int health = 3; // 플레이어 체력
    public Animator animator; // 플레이어 애니메이터
    public static SpriteRenderer playerRenderer;
    public bool isFallen = false;
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
        gameObject.SetActive(true);
    }
    private void Update()
    {
        // y축이 -6 이하일 경우, 오브젝트를 비활성화
        if (transform.position.y <= -6)
        {
            gameObject.SetActive(false);
            isFallen = true;
            health = 0;
            Debug.Log("떨어져 쥬금 ㅋ");

            GameManager.Inst.CheckGameOver();
            HealthUI.Inst.UpdateHeartsUI();

            GameManager.Inst.isGameOver = true;
        }
    }

    public IEnumerator FlickerCharacter()
    {
        playerRenderer.color = new Color(1, 0, 0, 0.7f);
        yield return new WaitForSeconds(0.1f);
        playerRenderer.color = Color.white;
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
