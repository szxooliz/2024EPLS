using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinInShop : MonoBehaviour
{    

    public SO_SkinInfo skinInfo;

    public TextMeshProUGUI txt_State; // 버튼 위 장착 or 보유 상태 표시 텍스트
    public TextMeshProUGUI txt_Price; // 버튼 위 코스튬 가격 텍스트
    public TextMeshProUGUI txt_SkinName; // 버튼 위 코스튬 이름 텍스트
    public Image img_Skin; // 버튼 위 보여지는 코스튬
    public GameObject btn_Lock; // 해금 전 회색 반투명 레이어
    public bool isSkinWorn; // 착용 여부


    [SerializeField] private bool isSkinUnlocked; // 해금 여부
    private IEnumerator openPopup; // 팝업 등장 코루틴용


    private void Start() 
    {
        isSkinWorn = PlayerPrefs.GetInt(SkinManager.lastSkin) == (int)skinInfo._skinID;
        isSkinUnlocked = PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1;
        InitializeBtn();
    }

    /// <summary>
    /// 버튼 초기화 세팅
    /// </summary>
    public void InitializeBtn()
    {
        img_Skin.sprite = skinInfo._skinSprite;
        txt_Price.text = skinInfo._skinPrice.ToString();
        txt_SkinName.text = skinInfo._skinName.ToString();
        IsSkinUnlocked();
    }
    
    /// <summary>
    /// 해금 상태에 따라 미해금 버튼 삭제, 상태 텍스트 변경
    /// </summary>
    public bool IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            Destroy(btn_Lock);
            ChangeStateText(isSkinWorn);
        }
        return isSkinUnlocked;
    }

    /// <summary>
    /// 미해금 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickLocked()
    {
        SkinManager.Inst.PreviewState(skinInfo);

        // 2nd click -> 구매 팝업 활성화
        if (SkinManager.Inst.isNowPreviewing)
        {
            openPopup = PopupManager.Inst.OpenPopup(PopupManager.Inst.popUp_Buy, 0f);

            StartCoroutine(openPopup);
            PopupManager.Inst.txt_PopupPrice.text = skinInfo._skinPrice.ToString();
        }
        else
        {
            // 1st click -> 미리보기 이미지 변경
            SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
            SkinManager.Inst.PreviewState(skinInfo);
        }
    }

    /// <summary>
    /// 해금된 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickUnlocked()
    {
        // 스킨이 해금되어 있으면
        if (isSkinUnlocked)
        {
            // 스킨을 착용하고 있는 상태이면
            if (isSkinWorn)
            {
                // 다른 미해금 코스튬을 미리보기 하고 있었다면
                if (SkinManager.Inst.isNowPreviewing)
                {
                    // 다시 착용 중이었던 스킨(클릭한 것)으로 돌아오기
                    SkinManager.Inst.EquipSkin(skinInfo);

                    SkinManager.Inst.ClosePreviewText(SkinManager.Inst.isNowPreviewing);
                    SkinManager.Inst.isNowPreviewing = false;

                    PopupManager.Inst.TakeOffSkin(SkinManager.Inst.isNowPreviewing);
                }
            }
            else
            {
                // 미리보기 스킨 변경
                SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
                SkinManager.Inst.ClosePreviewText(SkinManager.Inst.isNowPreviewing);

                // 착용하시겠습니까 팝업 열기
                openPopup = PopupManager.Inst.OpenPopup(PopupManager.Inst.popUp_Wear, 0f);
                StartCoroutine(openPopup);
            }
        }
    }

    /// <summary>
    /// 버튼 위 코스튬 착용 상태 텍스트 변경
    /// </summary>
    /// <param name="_isSkinWorn"></param>
    public void ChangeStateText(bool _isSkinWorn)
    {
        if(_isSkinWorn)
        {
            txt_State.text = "착용 중";
        }
        else
        {
            txt_State.text = "보유 중";
        }
    }
}