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
    private bool isDestroyed = false; // 선두 패턴이 삭제 되었는지 확인하는 용도

    public void Start()
    {
        Debug.Log("Game Start");

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
            Debug.Log("Setpattern___index" + index);

            // 스폰할 패턴 큐에 삽입 및 Instantiate
            GameObject patternInstance = Instantiate(patternToSpawn[index], startPosition, UnityEngine.Quaternion.identity);
            patternQueue.Enqueue(patternInstance);
            Debug.Log("Setpattern : " + patternInstance);
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
            Debug.Log("WaitUntil(() => firstPattern.transform.position.x < -66.89f) : " + (firstPattern.transform.position.x < -66.89f));
        }

        UnityEngine.Vector3 spawnPosition = new UnityEngine.Vector3(-19.3f, 5.121895f, 0f);

        if (patternToSpawn != null && patternToSpawn.Length > 0)
        {
            while(patternQueue.Count <= 3)
            {
                // 스폰할 패턴 랜덤으로 정하기
                int index = UnityEngine.Random.Range(0, patternToSpawn.Length); 
                Debug.Log("SpawnNewPattern___index" + index);

                // 스폰할 패턴 큐에 삽입 및 Instantiate
                GameObject patternInstance = Instantiate(patternToSpawn[index], spawnPosition, UnityEngine.Quaternion.identity);
                patternQueue.Enqueue(patternInstance);
                Debug.Log("SpawnPattern : " + patternInstance);

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
        // 활성화 된 패턴이 3개 초과일 때까지 기다림
        yield return new WaitUntil(() => patternQueue.Count > 3);
        Debug.Log("patternQueue.Count > 3 " + (patternQueue.Count > 3));
        
        // 이후 패턴 삭제 실행
        headPattern = patternQueue.Dequeue();
        Debug.Log("DestroyPattern " + headPattern + " ___isDestroyed : " + isDestroyed);
        Debug.Log("SpawnPattern : " + headPattern);

        Destroy(headPattern);

        isDestroyed = true;
    }
}