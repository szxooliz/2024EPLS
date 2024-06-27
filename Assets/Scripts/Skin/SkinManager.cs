using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{    
    public static SkinManager Inst;
    public static Sprite equippedSkin {get; private set;}
    [SerializeField] private SO_SkinInfo[] allSkins;
    private const string skinPref = "skinPref";

    private void Awake() 
    {
        // Singleton
        Inst = this;

        string lastSkinUsed = PlayerPrefs.GetString(skinPref, SO_SkinInfo.SkinIDS.defaultSkin.ToString());
        SO_SkinInfo skinUsedLastTime = Array.Find(allSkins, dummyFind => dummyFind._skinID.ToString() == lastSkinUsed);
        
        if (skinUsedLastTime == null)
        {
            skinUsedLastTime = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
        }
        
        EquipSkin(skinUsedLastTime);

    }

    public void EquipSkin(SO_SkinInfo skinInfo)
    {
        if (skinInfo == null)
        {
            skinInfo = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
        }

        equippedSkin = skinInfo._skinSprite;
        PlayerPrefs.SetString(skinPref, skinInfo._skinID.ToString());
    }
}