using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clover : MonoBehaviour
{
    [SerializeField] private float moveDistance = 5f; // 이동할 거리
    [SerializeField] private float duration = 2f; // 이동하는 데 걸리는 시간
    public Animator animator;

    private void Awake() 
    {
        animator = GetComponent<Animator>();    
    }

    private void OnTriggerEnter2D(Collider2D collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("______클로버 충돌!______");

            // 피격 애니메이션 발동
            animator.SetTrigger("Active");

            StartCoroutine(MoveAllPatterns());
            StartCoroutine(SpringPlayer());
        }
    }
    /// <summary>
    /// 큐에 있는 모든 오브젝트 이동 + 코루틴 일시정지 및 실행
    /// </summary>
    /// <returns></returns>
    public IEnumerator MoveAllPatterns()
    {
        Debug.Log("_____패턴 움직이기 시작!______");
        BackGroundLoop.instance.PauseMovement();

        foreach (GameObject pattern in PatternSpawn.Inst.patternQueue)
        {
            StartCoroutine(MovePattern(pattern));
        }

        foreach (GameObject backGround in BackGroundLoop.instance.backgrounds)
        {
            StartCoroutine(MovePattern(backGround));
        }

        yield return new WaitForSeconds(duration);
        Debug.Log("______패턴 움직이기 끝!______");

        BackGroundLoop.instance.ResumeMovement();
    }

    /// <summary>
    /// 개별 오브젝트를 오른쪽으로 이동
    /// </summary>
    /// <param name="pattern"></param>
    /// <returns></returns>
    public IEnumerator MovePattern(GameObject pattern)
    {
        Debug.Log("MovePattern : " + pattern);

        Vector3 startPos = pattern.transform.position;
        Vector3 endPos = startPos + Vector3.right * moveDistance;
        float elapsedTime = 0f;

        while (elapsedTime <  duration)
        {
            if (pattern != null)
            {
                pattern.transform.position = Vector3.Lerp(startPos, endPos, elapsedTime / duration);
                elapsedTime += Time.deltaTime;
            }
            else
            {
                yield break;
            }
            yield return null;
        }

        pattern.transform.position = endPos;
    }

    /// <summary>
    /// 플레이어 튀어오르는 효과
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpringPlayer()
    {
        Debug.Log("______플레이어 튀어오르기 시작!______");
        // 캐릭터의 원래 위치 저장
        Vector3 originalPosition = Player.Inst.transform.position;
        float knockBackHeight = 4.0f;  // y축으로 올라갈 높이
        float elapsedTime = 0f;

        while (elapsedTime <  duration)
        {
            float progress = elapsedTime / duration;
            float yOffset = knockBackHeight * Mathf.Sin(Mathf.PI * progress);
            
            Player.Inst.transform.position = new Vector3(originalPosition.x, originalPosition.y + yOffset, originalPosition.z);
            elapsedTime += Time.deltaTime;
            
            Debug.Log("______플레이어 튀어오르기 하는 중!______");
            yield return null;
        }
        Debug.Log("______플레이어 튀어오르기 끝!______");
        // 최종적으로 원래 위치로 돌아가기
        Player.Inst.transform.position = originalPosition;
    }
}