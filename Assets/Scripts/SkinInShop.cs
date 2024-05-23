using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinInShop : MonoBehaviour
{
    public SO_SkinInfo skinInfo;
    // public TextMeshProUGUI btnText;
    public Image skinImage;
    public Image previewImage;
    public bool isSkinUnlocked;

    private void Awake() 
    {
        // previewImage.sprite = 
        skinImage.sprite = skinInfo.skinSprite;
        IsSkinUnlocked();
    }
    public void IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo.skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            // btnText.text = "Equiped";
        }
    }

    public void OnButtonPress()
    {
        previewImage.sprite = skinInfo.skinSprite;
        if(isSkinUnlocked)
        {
            // equip skin
            FindObjectOfType<SkinManager>().EquipSkin(skinInfo);
        }
        else
        {
            // buy skin
            if (FindObjectOfType<PlayerCoin>().TryRemoveCoin(skinInfo.skinPrice))
            {
                PlayerPrefs.SetInt(skinInfo.skinID.ToString(), 1);
                IsSkinUnlocked();
            }
        }
    }
}
