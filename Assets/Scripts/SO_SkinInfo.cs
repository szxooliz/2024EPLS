using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName ="New Skin", menuName ="Create New Skin")]
public class SO_SkinInfo : ScriptableObject
{
    public enum SkinIDS { defaultSkin, santa, uniform }
    public SkinIDS skinID;

    public Sprite skinSprite;
    public int skinPrice;
}
