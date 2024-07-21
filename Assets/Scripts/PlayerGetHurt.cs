using System.Collections;
using UnityEngine;

public class PlayerGetHurt : MonoBehaviour
{
    /// <summary>
    /// 목숨이 깎일 때 0.1초동안 색깔 바뀜 (코루틴 이용)
    /// </summary>
    public static PlayerGetHurt Inst;
    private SpriteRenderer playerRenderer;
    private Color originalColor;
    
    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Inst == null)
        {
            Inst = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 객체 유지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스는 파괴
        }
    }

    void Start()
    {
        playerRenderer = GetComponent<SpriteRenderer>();

        // 원래 색상 저장
        if (playerRenderer != null)
        {
            originalColor = playerRenderer.color;
        }
    }

    public void GetHurt()
    {
        StartCoroutine(FlashRed());
    }

    private IEnumerator FlashRed()
    {
        if (playerRenderer != null)
        {
            playerRenderer.color = Color.red;
            yield return new WaitForSeconds(0.1f);
            playerRenderer.color = originalColor;
        }
    }
}
