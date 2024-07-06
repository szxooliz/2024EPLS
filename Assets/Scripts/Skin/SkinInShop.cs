using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinInShop : MonoBehaviour
{    

    public SO_SkinInfo skinInfo;

    public TextMeshProUGUI txt_State; // 버튼 위 장착 or 보유 상태 표시 텍스트
    // public TextMeshProUGUI txt_PopupPrice; // 팝업 위 코스튬 가격 텍스트
    public TextMeshProUGUI txt_Price; // 버튼 위 코스튬 가격 텍스트
    public TextMeshProUGUI txt_SkinName; // 버튼 위 코스튬 이름 텍스트
    public Image img_Skin; // 버튼 위 보여지는 코스튬

    public GameObject btn_Lock; // 해금 전 회색 반투명 레이어

    /*
    // 이 아래 전부 PopupManager로 옮기기 
    // public GameObject popUp_Buy; // 구매 재질문 팝업
    // public GameObject popUp_Caution; // 코인 부족 시 경고 팝업
    // public GameObject popUp_Wear; // 적용 질문 팝업
    // public GameObject popUp_Purchase; // 구매 완료 팝업
    // public GameObject panel; // 뒷 패널
    */
    public bool isSkinWorn = false; // 착용 여부
    // public bool isNowPreviewing = false; // 미리보기 여부

    [SerializeField] private bool isSkinUnlocked = false; // 해금 여부
    // [SerializeField] private float delayTime = 0.3f; // 팝업 지연 시간
    // private GameObject popUp_Active; // 현재 활성화 해둔 팝업 <- SkinManager.cs
    private IEnumerator openPopup; // 팝업 등장 코루틴용


    private void Awake() 
    {       
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
    }

    /*
    // ---------PopupManager.cs로 이동---------
    // /// <summary>
    // /// 팝업 열기
    // /// </summary>
    // public void OpenPopup()
    // {
    //     popUp_Active.SetActive(true);
    //     panel.SetActive(true);
    // }

    // /// <summary>
    // /// 팝업 닫기
    // /// </summary>
    // public void ClosePopup()
    // {
    //     // 미리보기 원상 복귀
    //     SkinManager.Inst.img_Preview.sprite = SkinManager.equippedSkin;

    //     if (popUp_Active != null)
    //     {
    //         popUp_Active.SetActive(false);
    //         panel.SetActive(false);
    //         ClosePreviewText(isNowPreviewing);
    //     }
    // }
    // ---------------------------

    // ---------SkinManager.cs로 이동---------
    // /// <summary>
    // /// 미리보기 텍스트 비활성화
    // /// </summary>
    // /// <param name="_isNowPreviewing"></param>
    // public void ClosePreviewText(bool _isNowPreviewing)
    // {
    //     // 미리보기 중이면 스킨 이름, 미리보기 상태 텍스트 비활성화
    //     if (_isNowPreviewing)
    //         {
    //             SkinManager.Inst.txt_preview.SetActive(false);
    //             SkinManager.Inst.txt_previewName.gameObject.SetActive(false);
    //         }
    // }
    */
    
    /// <summary>
    /// 해금 상태 확인
    /// </summary>
    public bool IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            // Debug.Log("SKININSHOP____" + skinInfo._skinName + " 보유 중 텍스트!");
        }
        Debug.Log("IsSkinUnlocked --- 해금 상태 변경 -> " + skinInfo._skinName + " " + skinInfo._skinID + " : " + PlayerPrefs.GetInt(skinInfo._skinID.ToString()));
        return isSkinUnlocked;
    }

    /// <summary>
    /// 미해금 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickLocked()
    {
        SkinManager.Inst.CheckPreview(skinInfo);
        Debug.Log("SkinInshop --- 선택한 스킨 : " + skinInfo._skinName.ToString());

        // 2nd click -> 구매 팝업 활성화
        if (SkinManager.Inst.isNowPreviewing)
        {
            // Debug.Log("두 번째 클릭");
            // PopupManager.Inst.popUp_Active = popUp_Buy;

            openPopup = PopupManager.Inst.OpenPopup(PopupManager.Inst.popUp_Buy, 0f);

            StartCoroutine(openPopup);
            PopupManager.Inst.txt_PopupPrice.text = skinInfo._skinPrice.ToString();
        }
        else
        {
            // 1st click -> 미리보기 이미지 변경
            // Debug.Log("첫 번째 클릭");
            SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
            SkinManager.Inst.txt_preview.SetActive(true);
        }

        // // 1st click -> 미리보기 이미지 변경
        // Debug.Log("첫 번째 클릭");
        // SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
        // SkinManager.Inst.txt_preview.SetActive(true);
    }

    /// <summary>
    /// 해금된 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickUnlocked()
    {
        // Debug.Log("isNowDefault /  지금 기본스킨 착용? : " + SkinManager.Inst.isNowDefault);
        Debug.Log("isSkinWorn /  지금 이 스킨 " + skinInfo._skinName + " 착용? : " + isSkinWorn);
        Debug.Log("SkinInShop --- OnClickUnlocked if문 실행 : " + (IsSkinUnlocked() && !isSkinWorn));

        if (IsSkinUnlocked() && !isSkinWorn)
        {
            // 미리보기 스킨 변경
            SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
            // 착용하시겠습니까 팝업 열기
            openPopup = PopupManager.Inst.OpenPopup(PopupManager.Inst.popUp_Wear, 0f);
            StartCoroutine(openPopup);
        }
        else
        {
            // 예외 처리
            return;
        }
    }

    /*
    // ---------PopupManager.cs로 옮기기---------
    /// <summary>
    /// 구매하시겠습니까 -> YES
    /// </summary>
    // public void OnClickBuyYes()
    // {
    //     // 구매 가능한 만큼 코인 보유 확인 및 차감
    //     bool ableToBuy = CoinManager.TryRemoveCoin(skinInfo._skinPrice);
    //     PopupManager.Inst.ClosePopup();

    //     if(ableToBuy)
    //     {
    //         // lock 버튼 삭제
    //         IsSkinUnlocked();
    //         Destroy(btn_Lock);

    //         // PlayerPrefs에 스킨 해금 정보 저장
    //         PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
    //         PlayerPrefs.Save();

    //         // 구매 완료 팝업 활성화
    //         popUp_Active = popUp_Purchase;
    //         Invoke("OpenPopup", delayTime);

    //         // txt_State.text = "보유 중";
    //         ChangeStateText(isSkinWorn);
    //     }
    //     else
    //     {
    //         // 코인 부족 팝업 활성화
    //         popUp_Active = popUp_Caution;
    //         Invoke("OpenPopup", delayTime);
    //     }
    // }
    
    /// <summary>
    /// 적용하시겠습니까 -> YES
    /// </summary>
    // public void OnClickWearYes()
    // {
    //     if(IsSkinUnlocked())
    //     {
    //         // 선택된 스킨 적용
    //         SkinManager.Inst.EquipSkin(skinInfo);
    //         isSkinWorn = true;

    //         Debug.Log("SKININSHOP____" + skinInfo._skinName + " 착용 중 텍스트!");
            
    //         // txt_State.text = "착용 중"; 
    //         ChangeStateText(isSkinWorn);
    //         ClosePopup();
    //     }
    //     else
    //     {
    //         ClosePopup();
    //         return;
    //     }
    // }
    // --------------------------------------------
    */

    /// <summary>
    /// 코스튬 착용 상태 텍스트 변경
    /// </summary>
    /// <param name="_isSkinWorn"></param>
    public void ChangeStateText(bool _isSkinWorn)
    {
        if(_isSkinWorn)
        {
            txt_State.text = "착용 중";
            Debug.Log("ChangeStateText --- 착용 중");
        }
        else
        {
            txt_State.text = "보유 중";
            Debug.Log("ChangeStateText --- 보유 중");
        }
    }
}