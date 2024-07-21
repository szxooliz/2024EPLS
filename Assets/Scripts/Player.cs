using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Inst; 
    public static int health = 3; // 플레이어 체력
    public Animator animator; // 플레이어 애니메이터

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
    }
}
