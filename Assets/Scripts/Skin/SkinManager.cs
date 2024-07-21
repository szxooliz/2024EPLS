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
    // public SkinInShop[] skinInShops;

    public Image img_Preview; // 미리보기 코스튬 파츠
    public TextMeshProUGUI txt_previewName; // 미리보기 코스튬 이름 안내 메시지
    public GameObject txt_preview; // 미리보기 안내 메시지
    
    public bool isNowDefault; // 코스튬 초기화 상태 여부 
    public bool isNowPreviewing = false; // 미리보기 여부

    [SerializeField] private SO_SkinInfo[] allSkins;
    private const string skinPref = "skinPref";
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
        
        // lastUsedID = PlayerPrefs.GetInt(skinPref, (int)SO_SkinInfo.SkinIDS.defaultSkin);
        // Skin.lastUsedSkin = Array.Find(skinInShops, dummyFind => (int)dummyFind.skinInfo._skinID == lastUsedID);
        foreach (SkinInShop skinInShop in Skin.Inst.skinInShops)
        {
            if (skinInShop.isSkinWorn)
            {
                Skin.lastUsedSkin = skinInShop;
                Debug.Log("SkinManager ___ isSkinWorn = True인 lastUsedSkin : " + skinInShop.skinInfo._skinName);
            }
        }

        if (Skin.lastUsedSkin == Skin.Inst.skinInShops[(int)SO_SkinInfo.SkinIDS.defaultSkin])
        {
            isNowDefault = true;
        }

        if (Skin.lastUsedSkin == null)
        {
            Skin.lastUsedSkin = Skin.Inst.skinInShops[(int)SO_SkinInfo.SkinIDS.defaultSkin];
            isNowDefault = true;
        }
    }

    private void Start() 
    {
        Debug.Log("SkinManager ___ Start ___ lastUsedSkin은? : " + Skin.lastUsedSkin.skinInfo._skinName);
        EquipSkin(Skin.lastUsedSkin.skinInfo);
        Debug.Log("SkinManager ___ Start ___ EquipSkin 실행");
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
        Debug.Log("SkinManager ___ 이미지 변경 " + Skin.lastUsedSkin.skinInfo._skinName + "로 합시다 제발");
        Skin.lastUsedSkin.skinInfo = _skinInfo;
        img_Preview.sprite = Skin.lastUsedSkin.skinInfo._skinSprite;
        PlayerPrefs.SetString(skinPref, _skinInfo._skinID.ToString());

        // Skin.lastUsedSkin.skinInfo = _skinInfo;
        Debug.Log("SkinManager ___ lastUsedSkin 그래서 뭔데.. EquipSkin 했잖아: " + Skin.lastUsedSkin.skinInfo._skinName);

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

    public void CheckPreview(SO_SkinInfo skinInfo)
    {
        // 현재 미리보기 이미지와 선택한 스킨이 같은지 확인
        isNowPreviewing = img_Preview.sprite == skinInfo._skinSprite;

        // 미리보기 이름 표시
        txt_previewName.gameObject.SetActive(true);
        txt_previewName.text = skinInfo._skinName;
    }
}