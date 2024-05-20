using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakePipe : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private float timeDiff; // 파이프 나오는 시간
    private float timer = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = new Vector3(10, Random.Range(-6.4f, -4.5f), 0); // 새로운 파이프 생성 위치
            timer = 0;
            Destroy(newpipe, 11.0f);
        }
    }
}
