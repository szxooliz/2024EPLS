using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

public class PatternSpawn : MonoBehaviour
{
    public static PatternSpawn Inst; // Singleton
    public GameObject[] patternToSpawn; // 패턴 프리팹 저장
    public Queue<GameObject> patternQueue = new Queue<GameObject>(); // 현재 스폰되어있는 패턴들
    private GameObject headPattern; // 삭제될 선두 패턴
    public float duration = 9f; // 패턴 유지 시간

    private void Awake() 
    {
        if (Inst == null)
        {
            Inst = this;
        }
    }
    public void Start()
    {
        Setpattern();
        StartCoroutine(SpawnNewPattern());
    }

    private void Update()
    {        
        if (patternQueue.Count > 3 || patternQueue.Peek().transform.position.x < -180f)
        {
            Debug.Log("큐 개수 : " + patternQueue.Count);
            StartCoroutine(DestroyPattern());
        }

        if(GameManager.Inst.isGameOver)
        {
            StopCoroutine(SpawnNewPattern());
            StopCoroutine(DestroyPattern());
            GameManager.Inst.CheckGameOver();
        }
    }

    /// <summary>
    /// 씬 로드 시 초기 패턴 세팅
    /// </summary>
    public void Setpattern()
    {
        UnityEngine.Vector3 startPosition = new UnityEngine.Vector3(-40f, 5.121895f, 0f);
   
        if (patternToSpawn != null && patternToSpawn.Length > 0)
        {
            // 스폰할 패턴 랜덤으로 정하기
            int index = UnityEngine.Random.Range(0, patternToSpawn.Length); 

            // 스폰할 패턴 큐에 삽입 및 Instantiate
            GameObject patternInstance = Instantiate(patternToSpawn[index], startPosition, UnityEngine.Quaternion.identity);
            patternQueue.Enqueue(patternInstance);
        }
    }

    /// <summary>
    /// 플레이 시간 내에 패턴 스폰
    /// </summary>
    /// <returns></returns>
    public IEnumerator SpawnNewPattern()
    {
        // 만약 Setpattern() 실행 직후라면
        if (patternQueue.Count == 1)
        {
            GameObject firstPattern = patternQueue.Peek();
            yield return new WaitUntil(() => firstPattern.transform.position.x < -66.89f);
        }

        UnityEngine.Vector3 spawnPosition = new UnityEngine.Vector3(-19.3f, 5.121895f, 0f);

        if (patternToSpawn != null && patternToSpawn.Length > 0)
        {
            GameObject previousPattern = patternQueue.Peek();

            while(patternQueue.Count <= 3)
            {
                // 스폰할 패턴 랜덤으로 정하기
                int index = UnityEngine.Random.Range(0, patternToSpawn.Length); 

                // 스폰할 패턴 큐에 삽입 및 Instantiate
                GameObject patternInstance = Instantiate(patternToSpawn[index], spawnPosition, UnityEngine.Quaternion.identity);
                patternQueue.Enqueue(patternInstance);

                yield return new WaitUntil(() => patternInstance.transform.position.x < -66.89f);
            }
        }
    }

    /// <summary>
    /// 플레이 시간 내에 패턴 삭제
    /// </summary>
    /// <returns></returns>
    public IEnumerator DestroyPattern()
    {
        // 활성화 된 패턴이 3개 초과일 때까지 대기
        yield return new WaitUntil(() => patternQueue.Count > 3);
        
        if (patternQueue.Count > 3)
        {
            // 이후 패턴 삭제 실행
            headPattern = patternQueue.Dequeue();
            Destroy(headPattern);
        }
    }

    /// <summary>
    /// 코루틴 시작
    /// </summary>
    public void StartSpawn()
    {
        StartCoroutine(DestroyPattern());
        StartCoroutine(SpawnNewPattern());
    }

    /// <summary>
    /// 코루틴 정지
    /// </summary>
    public void StopSpawn()
    {
        StopCoroutine(DestroyPattern());
        StopCoroutine(SpawnNewPattern());
    }
}