using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinManager : MonoBehaviour
{    
    public static SkinManager Inst;

    public Image img_Preview; // 미리보기 코스튬 파츠
    public static Sprite equippedSkin {get; private set;} // 장착한 코스튬 스프라이트

    [SerializeField] private SO_SkinInfo[] allSkins;
    private const string skinPref = "skinPref";

    private void Awake() 
    {
        // Singleton
        Inst = this;

        // 테스트 확인 용이하도록 추가한 코드 -> 출시 직전에 지우기
        PlayerPrefs.DeleteAll();

        string lastSkinUsed = PlayerPrefs.GetString(skinPref, SO_SkinInfo.SkinIDS.defaultSkin.ToString());
        SO_SkinInfo skinUsedLastTime = Array.Find(allSkins, dummyFind => dummyFind._skinID.ToString() == lastSkinUsed);
        
        if (skinUsedLastTime == null)
        {
            skinUsedLastTime = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
        }
        
        EquipSkin(skinUsedLastTime);
    }

    /// <summary>
    /// 선택한 코스튬 적용
    /// </summary>
    /// <param name="skinInfo"></param>
    public void EquipSkin(SO_SkinInfo skinInfo)
    {
        if (skinInfo == null)
        {
            skinInfo = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
        }

        equippedSkin = skinInfo._skinSprite;
        PlayerPrefs.SetString(skinPref, skinInfo._skinID.ToString());

        img_Preview.sprite = equippedSkin;
    }
}