using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable, CreateAssetMenu(fileName ="New Skin", menuName ="Create New Skin")]
public class SO_SkinInfo : ScriptableObject
{
    public enum SkinIDS { defaultSkin, santa, uniform, lemon }
    [SerializeField] private SkinIDS skinID;
    public SkinIDS _skinID { get { return skinID; } }

    [SerializeField] private Sprite skinSprite;
    public Sprite _skinSprite { get { return skinSprite; } }

    [SerializeField] private int skinPrice;
    public int _skinPrice { get { return skinPrice; } }
    [SerializeField] private string skinName;
    public string _skinName { get { return skinName; } }
}
