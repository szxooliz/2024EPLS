using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    // public SpriteRenderer playerSR;

    public RuntimeAnimatorController[] skinAnimators; // 스킨 별 애니메이션 컨트롤러
    public Animator animator;
    public int controllerID;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        controllerID = (int)SkinManager.lastUsedSkin.skinInfo._skinID;
        animator.runtimeAnimatorController = skinAnimators[controllerID];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
