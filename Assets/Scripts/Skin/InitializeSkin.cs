using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InitializeSkin : MonoBehaviour
{
    public SO_SkinInfo skinInfo_Default;
    public Button btn_initSkin; // 초기화 버튼

    void Awake() 
    {
        // 기본 코스튬은 항상 해금되어있음
        PlayerPrefs.SetInt(skinInfo_Default._skinID.ToString(), 1);
        PlayerPrefs.Save();

        btn_initSkin.onClick.AddListener(InitSkin);
    }

    /// <summary>
    /// 스킨 초기화 기능
    /// </summary>
    public void InitSkin()
    {
        // bool isNowInit = SkinManager.Inst.img_Preview.sprite == skinInfo_Default._skinSprite;
        
        if (SkinManager.Inst.isNowDefault)
        {
            // Debug.Log("이미 초기화 되어 있습니다 ~");
            return;
        }
        else
        {
            PopupManager.Inst.wear_SelectedNumber = (int)SO_SkinInfo.SkinIDS.defaultSkin;
            SkinManager.Inst.EquipSkin(skinInfo_Default);
            // Debug.Log("초기화!");
        }

        // 미리보기 상태였던 경우 -> 미리보기 상태 알림 비활성화
        SkinManager.Inst.ClosePreviewText(SkinManager.Inst.isNowPreviewing);

        // 최근 착용한 스킨 착용 해제 상태 만들기 
        // SkinManager.lastUsedSkin.isSkinWorn = false;
        SkinManager.lastUsedSkin.ChangeStateText(SkinManager.lastUsedSkin.isSkinWorn);

        // Debug.Log("lastUsedSkin 착용 해제");
        PopupManager.Inst.TakeOffSkin();
    }
}
