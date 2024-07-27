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
        // _CostumeScene 들어가지 않고 게임 시작하면 오류 뜸
        
        // if (Skin.lastUsedSkinInfo == null)
        // {
        //     Skin.lastUsedSkinInfo = SO_SkinInfo.SkinIDS.defaultSkin;
        //     SkinManager.Inst.isNowDefault = true;
        // }

        // Debug.Log("lastUsedSkin 대체 뭔데 : " + Skin.lastUsedSkinInfo._skinName);

        controllerID = PlayerPrefs.GetInt("lastSkin", (int)SO_SkinInfo.SkinIDS.defaultSkin);
        // controllerID = (int)Skin.lastUsedSkinInfo._skinID;
        
        animator.runtimeAnimatorController = skinAnimators[controllerID];
    }
}
