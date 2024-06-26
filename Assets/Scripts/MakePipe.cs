using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class MakePipe : MonoBehaviour
{
    [SerializeField] private GameObject pipe;
    [SerializeField] private float timeDiff; // 파이프 나오는 시간
    private float timer = 0;

    //hyoju
    [SerializeField] private GameObject obstacle;
    //hyoju end

    private float timeDiffTimer;
    private float acceleration;
    // Start is called before the first frame update
    void Start()
    {
        acceleration = BackGroundLoop.acceleration;
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
            newobstacle.transform.position = new Vector3(Random.Range(10, 30), 4, 0);
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
