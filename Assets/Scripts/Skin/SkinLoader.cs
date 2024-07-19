using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    // public SpriteRenderer playerSR;

    public RuntimeAnimatorController[] skinAnimators; // 스킨 별 애니메이션 컨트롤러
    public Animator animator; // Player의 컴포넌트
    public int controllerID;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
   void Start()
    {
        controllerID = (int)SkinManager.lastUsedSkin.skinInfo._skinID;
        animator.runtimeAnimatorController = skinAnimators[controllerID];
    }
}
