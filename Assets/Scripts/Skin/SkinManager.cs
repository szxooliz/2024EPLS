using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{    
    public static SkinManager Inst;
    // public static SkinInShop lastUsedSkin; // 가장 마지막에 착용한 코스튬 << SkinLoader로 옮김
    public SkinInShop[] skinInShops;

    public Image img_Preview; // 미리보기 코스튬 파츠
    public TextMeshProUGUI txt_previewName; // 미리보기 코스튬 이름 텍스트
    public GameObject txt_preview; // 미리보기 안내 메시지
    
    public bool isNowDefault; // 코스튬 초기화 상태 여부 
    public bool isNowPreviewing = false; // 미리보기 여부

    [SerializeField] private SO_SkinInfo[] allSkins;
    private const string lastSkin = "lastSkin";
    private int lastUsedID;

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
        // #if UNITY_EDITOR
        // PlayerPrefs.DeleteAll();
        // #endif
        
        LastUsedSkin();
        
        /*
        // foreach (SkinInShop skinInShop in skinInShops)
        // {
        //     if (skinInShop.isSkinWorn)
        //     {
        //         Skin.lastUsedSkin = skinInShop;
        //     }
        // }

        // if (Skin.lastUsedSkin == skinInShops[(int)SO_SkinInfo.SkinIDS.defaultSkin])
        // {
        //     isNowDefault = true;
        // }

        // if (Skin.lastUsedSkin == null)
        // {
        //     Skin.lastUsedSkin = skinInShops[(int)SO_SkinInfo.SkinIDS.defaultSkin];
        //     isNowDefault = true;
        // } */
    }

    private void Start() 
    {
        EquipSkin(Skin.lastUsedSkin.skinInfo);
    }

    public void LastUsedSkin()
    {
        lastUsedID = PlayerPrefs.GetInt(lastSkin, (int)SO_SkinInfo.SkinIDS.defaultSkin);
        Skin.lastUsedSkin = Array.Find(skinInShops, dummyFind => (int)dummyFind.skinInfo._skinID == lastUsedID);
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

    public void EquipSkin(SO_SkinInfo _skinInfo)
    {
        if (_skinInfo == null)
        {
            // 기본 스킨으로 초기화
            _skinInfo = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
            isNowDefault = true;
        }

        // 미리보기 이미지 적용
        // Skin.lastUsedSkin.skinInfo = _skinInfo;
        img_Preview.sprite = _skinInfo._skinSprite;
        // Skin.lastUsedSkin.skinInfo = _skinInfo;

        // PlayerPrefs에 착용한 스킨 아이디 저장
        PlayerPrefs.SetInt(lastSkin, (int)_skinInfo._skinID);

        Debug.Log("lastUsedSkin 그래서 뭔데.. EquipSkin 했잖아: " + Skin.lastUsedSkin.skinInfo._skinName);

        CheckInit();
    }

    /// <summary>
    /// 현재 초기화 상태인지 ID 비교 체크 및 isNowDefault 값 변경 
    /// </summary>
    /// <param name="skinInfo"></param>
    public void CheckInit()
    {
        if(Skin.lastUsedSkin.skinInfo._skinID == SO_SkinInfo.SkinIDS.defaultSkin)
        {
            isNowDefault = true;
        }
        else
        {
            isNowDefault = false;
        }
    }

    // public void CheckPreview(SO_SkinInfo skinInfo)
    // {
    //     // 현재 미리보기 이미지와 선택한 스킨이 같은지 확인
    //     isNowPreviewing = img_Preview.sprite == skinInfo._skinSprite;
    
    //     // 미리보기 이름 표시
    //     txt_previewName.gameObject.SetActive(isNowPreviewing);
    //     txt_previewName.text = skinInfo._skinName;
    // }

    /// <summary>
    /// isNowPreviewing 판정에 따른 미리보기 상태 오브젝트 활성화 결정
    /// </summary>
    /// <param name="skinInfo"></param>
    public void PreviewState(SO_SkinInfo skinInfo)
    {
        // 현재 미리보기 이미지와 선택한 스킨이 같은지 확인
        isNowPreviewing = img_Preview.sprite == skinInfo._skinSprite;

        // isNowPreviewing이 false이면 (기존에 미리보기 하고있었으면) 
        // 미리보기 이름 & 상태 오브젝트 표시
        txt_preview.SetActive(isNowPreviewing);
        txt_previewName.gameObject.SetActive(isNowPreviewing);
        txt_previewName.text = skinInfo._skinName;
    }
}