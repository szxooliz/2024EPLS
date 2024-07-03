using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{    
    public static SkinManager Inst;
    // public static Sprite equippedSkin {get; private set;} // 장착한 코스튬 스프라이트
    public static SkinInShop lastUsedSkin; // 가장 마지막에 착용한 코스튬 
    public SkinInShop[] skinInShops;

    public Image img_Preview; // 미리보기 코스튬 파츠
    public TextMeshProUGUI txt_previewName; // 미리보기 코스튬 이름 안내 메시지
    public GameObject txt_preview; // 미리보기 안내 메시지
    
    public bool isNowDefault; // 코스튬 초기화 상태 여부 
    public bool isNowPreviewing = false; // 미리보기 여부

    [SerializeField] private SO_SkinInfo[] allSkins;
    private const string skinPref = "skinPref";

    private void Awake() 
    {
        // Singleton
        if (Inst != null && Inst != this)
        {
            Destroy(gameObject);
            return;
        }
        Inst = this;

        // 유니티 에디터에서 테스트 용이하도록 추가한 코드
        #if UNITY_EDITOR
        PlayerPrefs.DeleteAll();
        #endif

        // isNowDefault = img_Preview.sprite == allSkins[(int)SO_SkinInfo.SkinIDS.defaultSkin];

        // if (isNowDefault)
        // {
        //     lastUsedSkin.skinInfo = 
        // }
        // lastUsedSkin = 

        
        string lastSkinUsed = PlayerPrefs.GetString(skinPref, SO_SkinInfo.SkinIDS.defaultSkin.ToString());
        SO_SkinInfo skinUsedLastTime = Array.Find(allSkins, dummyFind => dummyFind._skinID.ToString() == lastSkinUsed);
        
        if (skinUsedLastTime == null)
        {
            skinUsedLastTime = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
        }
        
        EquipSkin(skinUsedLastTime);
        
    }

    /// <summary>
    /// 미리보기 텍스트 비활성화
    /// </summary>
    /// <param name="_isNowPreviewing"></param>
    public void ClosePreviewText(bool _isNowPreviewing)
    {
        // 현재 상태가 미리보기 중이면 스킨 이름, 미리보기 상태 텍스트 비활성화
        if (_isNowPreviewing)
            {
                txt_preview.SetActive(false);
                txt_previewName.gameObject.SetActive(false);
            }
    }

    /// <summary>
    /// 선택한 코스튬 적용
    /// </summary>
    /// <param name="skinInfo"></param>
    // public void EquipSkin(SkinInShop _skinInShop)
    // {
    //     if (_skinInShop == null)
    //     {
    //         // 기본 스킨으로 초기화
    //         _skinInShop.skinInfo = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
    //         isNowDefault = true;
    //     }

    //     lastUsedSkin = _skinInShop;
    //     img_Preview.sprite = lastUsedSkin.skinInfo._skinSprite;

    //     // equippedSkin = _skinInShop.skinInfo._skinSprite;
    //     // img_Preview.sprite = equippedSkin;

    //     PlayerPrefs.SetString(skinPref, _skinInShop.skinInfo._skinID.ToString());

    //     CheckInit(_skinInShop.skinInfo);
    //     _skinInShop.ClosePreviewText(_skinInShop.isNowPreviewing);
    //     _skinInShop.ChangeStateText(isNowDefault);
    //     /*
    //     if(skinInfo._skinID == SO_SkinInfo.SkinIDS.defaultSkin)
    //     {
    //         isNowDefault = true;
    //     }
    //     else
    //     {
    //         isNowDefault = false;
    //     }
    //     */
    // }

    public void EquipSkin(SO_SkinInfo _skinInfo)
    {
        if (_skinInfo == null)
        {
            // 기본 스킨으로 초기화
            _skinInfo = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
            isNowDefault = true;
        }

        // 미리보기 이미지 적용
        // equippedSkin = _skinInfo._skinSprite;
        // img_Preview.sprite = equippedSkin;
        lastUsedSkin.skinInfo = _skinInfo;
        img_Preview.sprite = lastUsedSkin.skinInfo._skinSprite;
        PlayerPrefs.SetString(skinPref, _skinInfo._skinID.ToString());

        lastUsedSkin.skinInfo = _skinInfo;

        CheckInit(_skinInfo);
        /*
        if(skinInfo._skinID == SO_SkinInfo.SkinIDS.defaultSkin)
        {
            isNowDefault = true;
        }
        else
        {
            isNowDefault = false;
        }
        */
    }

    /// <summary>
    /// 현재 초기화 상태인지 ID 비교 체크 및 isNowDefault 값 변경 
    /// </summary>
    /// <param name="skinInfo"></param>
    public void CheckInit(SO_SkinInfo skinInfo)
    {
        if(lastUsedSkin.skinInfo._skinID == SO_SkinInfo.SkinIDS.defaultSkin)
        {
            isNowDefault = true;
        }
        else
        {
            isNowDefault = false;
        }
    }

    public void CheckPreview(SO_SkinInfo skinInfo)
    {
        // 현재 미리보기 이미지와 선택한 스킨이 같은지 확인
        isNowPreviewing = img_Preview.sprite == skinInfo._skinSprite;

        Debug.Log("SkinManager --- isNowPreviewing : " + isNowPreviewing);

        // 미리보기 이름 표시
        txt_previewName.gameObject.SetActive(true);
        txt_previewName.text = skinInfo._skinName;
    }
}