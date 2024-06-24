using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{    
    public static SkinManager Inst;
    public static Sprite equippedSkin;
    public SO_SkinInfo[] allSkins;

    private void Awake() 
    {
        // Singleton
        Inst = this;

        string lastSkinUsed = PlayerPrefs.GetString("skinPref", SO_SkinInfo.SkinIDS.santa.ToString());
        foreach (SO_SkinInfo skin in allSkins)
        {
            if (skin.skinID.ToString() == lastSkinUsed)
            {
                EquipSkin(skin);
            }
        }
    }

    public void EquipSkin(SO_SkinInfo skinInfo)
    {
        equippedSkin = skinInfo.skinSprite;
        PlayerPrefs.SetString("skinPref", skinInfo.skinID.ToString());
    }
}