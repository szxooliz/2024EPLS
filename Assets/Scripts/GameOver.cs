using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public RectTransform popupRect;
    public Vector2 startPos; // 출발 시작 위치
    public Vector2 endPos; // 최종 도착 위치
    public float duration = 0.3f; // 애니메이션 지속 시간
    public float delay = 0.3f; // 지연 시간

    void Start()
    {
        startPos = new Vector2(0, 225);
        endPos = new Vector2(0, -225);

        popupRect.anchoredPosition = startPos;

        if(Player.health <= 0)
            StartCoroutine(StartWithDelay(delay));
    }

    IEnumerator StartWithDelay(float delay)
    {
        Time.timeScale = 0f;

        // 지연 시간만큼 대기
        yield return new WaitForSecondsRealtime(delay);

        // 애니메이션 시작
        StartCoroutine(SlideDown());
    }

    /// <summary>
    /// 게임 오버 팝업 슬라이딩 코루틴
    /// </summary>
    /// <returns></returns>
    IEnumerator SlideDown()
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // 경과 시간 비율 계산
            float t = elapsedTime / duration;

            // 선형 보간으로 위치 계산
            popupRect.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

            elapsedTime += Time.unscaledDeltaTime;
            yield return null;
        }

        popupRect.anchoredPosition = endPos;
    }
}
