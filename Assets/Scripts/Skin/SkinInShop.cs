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
        // 이건 뭐지
        // isSkinWorn = Skin.lastUsedSkin.skinInfo._skinID == skinInfo._skinID;
        isSkinWorn = PlayerPrefs.GetInt("lastSkin") == (int)skinInfo._skinID;
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
        // SkinManager.Inst.CheckPreview(skinInfo);
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
            // SkinManager.Inst.txt_preview.SetActive(true);
            SkinManager.Inst.PreviewState(skinInfo);
        }
    }

    /// <summary>
    /// 해금된 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickUnlocked()
    {
        Debug.Log("클릭한 스킨 버튼 : " + skinInfo._skinName);
        
        if (isSkinUnlocked && !isSkinWorn)
        {
            Debug.Log(skinInfo._skinName + "___ IsSkinUnlocked : " + isSkinUnlocked + " | !isSkinWorn : " + !isSkinWorn);
            // 미리보기 스킨 변경
            SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;

            // 착용하시겠습니까 팝업 열기
            openPopup = PopupManager.Inst.OpenPopup(PopupManager.Inst.popUp_Wear, 0f);
            StartCoroutine(openPopup);
        }
        else if (isSkinUnlocked && isSkinWorn && SkinManager.Inst.isNowPreviewing)
        {   
            Debug.Log(skinInfo._skinName + "___ IsSkinUnlocked : " + isSkinUnlocked + " | isSkinWorn : " + isSkinWorn + " | SkinManager.Inst.isNowPreviewing : " + SkinManager.Inst.isNowPreviewing);
            // 미해금 스킨 미리보기 중에 다시 착용 스킨으로 돌아오기
            SkinManager.Inst.EquipSkin(skinInfo);
            // SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
            // SkinManager.Inst.CheckPreview(skinInfo);
            // SkinManager.Inst.PreviewState(skinInfo);
            Debug.Log(skinInfo._skinName + " isNowPreviewing : " + SkinManager.Inst.isNowPreviewing);

            SkinManager.Inst.ClosePreviewText(SkinManager.Inst.isNowPreviewing);
            SkinManager.Inst.isNowPreviewing = false;
            /// SkinManager.Inst.ClosePreviewText(SkinManager.Inst.isNowPreviewing);

            PopupManager.Inst.TakeOffSkin();
        }


    }

    /// <summary>
    /// 코스튬 착용 상태 텍스트 변경
    /// </summary>
    /// <param name="_isSkinWorn"></param>
    public void ChangeStateText(bool _isSkinWorn)
    {
        if(_isSkinWorn)
        {
            Debug.Log(skinInfo._skinName + " 착용 중 텍스트");
            txt_State.text = "착용 중";
        }
        else
        {
            Debug.Log(skinInfo._skinName + " 보유 중 텍스트");
            txt_State.text = "보유 중";
        }
    }
}