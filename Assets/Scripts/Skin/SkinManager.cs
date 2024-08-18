using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkinManager : MonoBehaviour
{    
    public static SkinManager Inst;
    public SkinInShop[] skinInShops;

    public Image img_Preview; // 미리보기 코스튬 파츠
    public TextMeshProUGUI txt_previewName; // 미리보기 코스튬 이름 텍스트
    public GameObject txt_preview; // 미리보기 안내 메시지
    
    public bool isNowDefault; // 코스튬 초기화 상태 여부 
    public bool isNowPreviewing = false; // 미리보기 여부

    [SerializeField] private SO_SkinInfo[] allSkins;
    public const string lastSkin = "lastSkin";
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
        LastUsedSkin();
    }

    private void Start() 
    {
        EquipSkin(Skin.lastUsedSkin.skinInfo);
    }
    
    /// <summary>
    /// 마지막에 적용한 스킨 불러오기
    /// </summary>
    public void LastUsedSkin()
    {
        Debug.Log("lastUsedID : " + PlayerPrefs.GetInt(lastSkin));
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

    /// <summary>
    /// PlayerPrefs에 착용한 스킨 아이디 저장
    /// </summary>
    /// <param name="_skinInfo"></param>
    public void EquipSkin(SO_SkinInfo _skinInfo)
    {
        if (_skinInfo == null)
        {
            // 기본 스킨으로 초기화
            _skinInfo = Array.Find(allSkins, dummyFind => dummyFind._skinID == SO_SkinInfo.SkinIDS.defaultSkin);
            isNowDefault = true;
        }

        // 미리보기 이미지 적용
        img_Preview.sprite = _skinInfo._skinSprite;

        // PlayerPrefs에 착용한 스킨 아이디 저장
        PlayerPrefs.SetInt(lastSkin, (int)_skinInfo._skinID);
        Skin.lastUsedSkin = skinInShops[PlayerPrefs.GetInt(lastSkin)];
        CheckInit();
    }

    /// <summary>
    /// 현재 초기화 상태인지 ID 체크
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