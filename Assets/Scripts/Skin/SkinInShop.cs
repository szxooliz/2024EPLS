using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinInShop : MonoBehaviour
{    

    public SO_SkinInfo skinInfo;
    public TextMeshProUGUI txt_State; // 버튼 위 장착 or 보유 상태 표시 텍스트
    public TextMeshProUGUI txt_PopupPrice; // 팝업 위 코스튬 가격 텍스트
    public TextMeshProUGUI txt_Price; // 버튼 위 코스튬 가격 텍스트
    public Image img_Skin; // 버튼 위 보여지는 코스튬
    public Image img_Preview; // 미리보기 코스튬 파츠
    public GameObject btn_Lock; // 해금 전 회색 반투명 레이어
    public GameObject popUp_Buy; // 구매 재질문 팝업
    public GameObject popUp_Caution; // 코인 부족 시 경고 팝업
    public GameObject popUp_Wear; // 적용 질문 팝업
    public GameObject popUp_Purchase; // 구매 완료 팝업
    public GameObject popUp_Active; // 현재 활성화 해둔 팝업
    public GameObject txt_preview; // 미리보기 안내 메시지
    public bool isNowPreviewing; // 미리보기 적용 중 여부 

    [SerializeField]
    private bool isSkinUnlocked = false; // 해금 여부


    private void Awake() 
    {       
        img_Skin.sprite = skinInfo.skinSprite;
        img_Preview.sprite = SkinManager.equippedSkin;
        txt_Price.text = skinInfo.skinPrice.ToString();
    }

    /// <summary>
    /// 코스튬 잠금 풀기
    /// </summary>
    public bool IsSkinUnlocked()
    {
        if (PlayerPrefs.GetInt(skinInfo.skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            txt_State.text = "bought"; // "보유 중" 텍스트
            Debug.Log("SKININSHOP____isSkinUnlocked: " + isSkinUnlocked);
        }
        return isSkinUnlocked;
    }

    /// <summary>
    /// 미해금 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickLocked()
    {
        // 현재 미리보기 이미지와 선택한 스킨이 같은지 확인
        isNowPreviewing = img_Preview.sprite == skinInfo.skinSprite;

        // 2nd click -> 구매 팝업 활성화
        if (isNowPreviewing)
        {
            popUp_Active = popUp_Buy;
            popUp_Buy.SetActive(true);
            txt_PopupPrice.text = skinInfo.skinPrice.ToString();
        }

        // 1st click -> 미리보기 이미지 변경
        img_Preview.sprite = skinInfo.skinSprite;
        txt_preview.SetActive(true);
    }

    /// <summary>
    /// 해금된 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickUnlocked()
    {
        if (IsSkinUnlocked())
        {
            img_Preview.sprite = skinInfo.skinSprite;
            popUp_Active = popUp_Wear;
            popUp_Wear.SetActive(true);
        }
        else
        {
            // 예외 처리
            return;
        }
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        // 미리보기 원상 복귀
        img_Preview.sprite = SkinManager.equippedSkin;
        popUp_Active.SetActive(false);
        txt_preview.SetActive(false);
    }

    /// <summary>
    /// 구매하시겠습니까 -> YES
    /// </summary>
    public void OnClickBuyYes()
    {
        // 구매 가능한 만큼 코인 보유 확인 및 차감
        bool ableToBuy = PlayerCoin.TryRemoveCoin(skinInfo.skinPrice);
        ClosePopup();

        if(ableToBuy)
        {
            // 차감된 코인 표시 코드

            // lock 버튼 삭제 및 구매 완료 팝업 활성화
            IsSkinUnlocked();
            Destroy(btn_Lock);

            popUp_Active = popUp_Purchase;
            popUp_Purchase.SetActive(true);

            Debug.Log("SKININSHOP____successful purchase!");
        }
        else
        {
            // 코인 부족 팝업 활성화
            popUp_Active = popUp_Caution;
            popUp_Caution.SetActive(true);

            Debug.Log("SKININSHOP____purchase fail...");
        }
    }
    
    /// <summary>
    /// 적용하시겠습니까 -> YES
    /// </summary>
    public void OnClickWearYes()
    {
        if(IsSkinUnlocked())
        {
            // 선택된 스킨 적용
            SkinManager.Inst.EquipSkin(skinInfo);
            img_Preview.sprite = SkinManager.equippedSkin;
            txt_State.text = "Equiped"; // "적용됨" 텍스트
            ClosePopup();

            Debug.Log("SKININSHOP____successful wear!");
        }
        else
        {
            Debug.Log("SKININSHOP____wear fail...");
            ClosePopup();
            return;
        }
    }
}