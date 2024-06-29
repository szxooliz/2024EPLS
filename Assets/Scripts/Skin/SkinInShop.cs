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
    public TextMeshProUGUI txt_SkinName; // 버튼 위 코스튬 이름 텍스트
    public TextMeshProUGUI txt_previewName; // 미리보기 코스튬 이름 안내 메시지

    public Image img_Skin; // 버튼 위 보여지는 코스튬

    public GameObject btn_Lock; // 해금 전 회색 반투명 레이어
    public GameObject popUp_Buy; // 구매 재질문 팝업
    public GameObject popUp_Caution; // 코인 부족 시 경고 팝업
    public GameObject popUp_Wear; // 적용 질문 팝업
    public GameObject popUp_Purchase; // 구매 완료 팝업
    public GameObject txt_preview; // 미리보기 안내 메시지
    public GameObject panel; // 뒷 패널
    public bool isSkinWorn = false; // 착용 여부

    [SerializeField] private bool isSkinUnlocked = false; // 해금 여부
    [SerializeField] private float delayTime = 0.3f; // 팝업 지연 시간
    private GameObject popUp_Active; // 현재 활성화 해둔 팝업


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

    /// <summary>
    /// 팝업 열기
    /// </summary>
    public void OpenPopup()
    {
        popUp_Active.SetActive(true);
        panel.SetActive(true);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        // 미리보기 원상 복귀
        SkinManager.Inst.img_Preview.sprite = SkinManager.equippedSkin;

        if (popUp_Active != null)
        {
            popUp_Active.SetActive(false);
            txt_preview.SetActive(false);
            panel.SetActive(false);
            txt_previewName.gameObject.SetActive(false);
        }
    }
    
    /// <summary>
    /// 해금 상태 확인
    /// </summary>
    public bool IsSkinUnlocked()
    {
        Debug.Log("해금 상태 확인 " + skinInfo._skinName + skinInfo._skinID + " : " + PlayerPrefs.GetInt(skinInfo._skinID.ToString()));
        
        if (PlayerPrefs.GetInt(skinInfo._skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;

            // Debug.Log("SKININSHOP____" + skinInfo._skinName + " 보유 중 텍스트!");
            // txt_State.text = "보유 중"; 
        }
        return isSkinUnlocked;
    }

    /// <summary>
    /// 미해금 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickLocked()
    {
        // 현재 미리보기 이미지와 선택한 스킨이 같은지 확인
        bool isNowPreviewing = SkinManager.Inst.img_Preview.sprite == skinInfo._skinSprite;

        // 미리보기 이름 표시
        txt_previewName.gameObject.SetActive(true);
        txt_previewName.text = skinInfo._skinName;

        // 2nd click -> 구매 팝업 활성화
        if (isNowPreviewing)
        {
            popUp_Active = popUp_Buy;
            OpenPopup();

            txt_PopupPrice.text = skinInfo._skinPrice.ToString();
        }

        // 1st click -> 미리보기 이미지 변경
        SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
        txt_preview.SetActive(true);

    }

    /// <summary>
    /// 해금된 코스튬 버튼 클릭 시
    /// </summary>
    public void OnClickUnlocked()
    {
        if (IsSkinUnlocked() && !isSkinWorn)
        {
            SkinManager.Inst.img_Preview.sprite = skinInfo._skinSprite;
            popUp_Active = popUp_Wear;
            OpenPopup();
        }
        else
        {
            // 예외 처리
            return;
        }
    }

    /// <summary>
    /// 구매하시겠습니까 -> YES
    /// </summary>
    public void OnClickBuyYes()
    {
        // 구매 가능한 만큼 코인 보유 확인 및 차감
        bool ableToBuy = Coin.TryRemoveCoin(skinInfo._skinPrice);
        ClosePopup();

        if(ableToBuy)
        {
            // lock 버튼 삭제
            IsSkinUnlocked();
            Destroy(btn_Lock);

            // PlayerPrefs에 스킨 해금 정보 저장
            PlayerPrefs.SetInt(skinInfo._skinID.ToString(), 1);
            PlayerPrefs.Save();

            // 구매 완료 팝업 활성화
            popUp_Active = popUp_Purchase;
            Invoke("OpenPopup", delayTime);

            txt_State.text = "보유 중";
        }
        else
        {
            // 코인 부족 팝업 활성화
            popUp_Active = popUp_Caution;
            Invoke("OpenPopup", delayTime);
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
            isSkinWorn = true;

            Debug.Log("SKININSHOP____" + skinInfo._skinName + " 착용 중 텍스트!");
            txt_State.text = "착용 중"; 
            ClosePopup();
        }
        else
        {
            ClosePopup();
            return;
        }
    }
}