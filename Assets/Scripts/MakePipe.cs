using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class MakePipe : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private float timeDiff; // 파이프 나오는 시간
    private float timer = 0;

    //hyoju : 덩굴이 1/n 확률로, 파이프와 위치가 안 겹치게끔 생성된다.
    [SerializeField] private GameObject obstacle;
    [SerializeField] private float probability;
    //hyoju end

    private float timeDiffTimer;
    private float acceleration;

    //1/n확률로 덩굴이 발생한다.
    private void ProbabilityRespawn(GameObject newobstacle)
    {
        float p = Random.value;
        if( p > probability)
        {
            newobstacle.transform.position = new Vector3(Random.Range(14, 17), 4, 0);
            // x축 생성 범위를 10-20에서 해야함!!!
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        acceleration = BackGroundLoop.acceleration;
        // n을 1/n으로 바꿔준다. 소수점 2점에서 버림.
        probability = Mathf.Floor(1 / probability * 100f) / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            GameObject newpipe = Instantiate(pipe);
            newpipe.transform.position = new Vector3(10, Random.Range(-6.5f, -4f), 0);// 새로운 파이프 생성 위치

            //hyoju inset 
            GameObject newobstacle = Instantiate(obstacle);
            ProbabilityRespawn (newobstacle);
            //hyoju end

            timer = 0;
            Destroy(newpipe, 11.0f);

            //hyoju insert
            Destroy(newobstacle, 11.0f);
            //hyoju end
        }

        if (timeDiffTimer > acceleration)
        {
            timeDiff -= 0.1f;
        }
    }
}
