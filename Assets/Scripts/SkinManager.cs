using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinManager : MonoBehaviour
{    public static Sprite equippedSkin;

    public void EquipSkin(SO_SkinInfo skinInfo)
    {
        equippedSkin = skinInfo.skinSprite;
    }
}
