using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    // public SpriteRenderer playerSR;
    // public static SkinLoader Inst;
    // public static SkinInShop lastUsedSkin; // 가장 마지막에 착용한 코스튬
    public RuntimeAnimatorController[] skinAnimators; // 스킨 별 애니메이션 컨트롤러
    public Animator animator; // Player의 컴포넌트
    public int controllerID;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
   void Start()
    {
        // PlayerPrefs 이용해서 마지막으로 적용한 스킨 불러오기
        controllerID = PlayerPrefs.GetInt("lastSkin", (int)SO_SkinInfo.SkinIDS.defaultSkin);        
        animator.runtimeAnimatorController = skinAnimators[controllerID];
    }
}
