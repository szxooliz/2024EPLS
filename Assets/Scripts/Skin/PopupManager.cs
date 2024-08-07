using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Inst; // Singleton
    public Button[] btns_Locked;
    public Button[] btns_Unlocked;

    public GameObject popUp_Buy; // 구매 재질문 팝업
    public GameObject popUp_Caution; // 코인 부족 시 경고 팝업
    public GameObject popUp_Wear; // 적용 질문 팝업
    public GameObject popUp_Purchase; // 구매 완료 팝업
    public GameObject panel; // 뒷 패널
    public TextMeshProUGUI txt_PopupPrice; // 팝업 위 코스튬 가격 텍스트

    public Button btn_BuyYes;
    public Button btn_WearYes;
    public Button btn_WearNo;
    [SerializeField] private int buy_SelectedNumber;
    public int wear_SelectedNumber;
    [SerializeField] private float delayTime = 0.3f; // 팝업 지연 시간
    private GameObject popUp_Active; // 현재 활성화 해둔 팝업
    private IEnumerator openPopup; // 팝업 등장 코루틴용

    void Awake()
    {
        // Singleton
        if(Inst == null) 
        {
            Inst = this;   
        }
        else
        {
            Destroy(gameObject);
        }
    

        foreach (Button btn_Locked in btns_Locked)
        {
            btn_Locked.onClick.AddListener(() => OnClickLockedBtn(btn_Locked));
        }

        foreach (Button btn_Unlocked in btns_Unlocked)
        {
            btn_Unlocked.onClick.AddListener(() => OnClickUnlockedBtn(btn_Unlocked));
        }

        btn_BuyYes.onClick.AddListener(() => OnClickBuyYes(buy_SelectedNumber));
        btn_WearYes.onClick.AddListener(() => OnClickWearYes(wear_SelectedNumber));
        btn_WearNo.onClick.AddListener(() => OnClickWearNo(wear_SelectedNumber));
    }

    /// <summary>
    /// 팝업 열기
    /// </summary>
    public IEnumerator OpenPopup(GameObject popUp, float delay)
    {
        popUp_Active = popUp;
        popUp_Active.SetActive(true);
        panel.SetActive(true);

        yield return new WaitForSeconds(delay);
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        // 미리보기 원상 복귀
        SkinManager.Inst.LastUsedSkin();
        SkinManager.Inst.img_Preview.sprite = Skin.lastUsedSkin.skinInfo._skinSprite;

        if (popUp_Active != null)
        {
            popUp_Active.SetActive(false);
            panel.SetActive(false);
            SkinManager.Inst.ClosePreviewText(SkinManager.Inst.isNowPreviewing);
        }
    }

    /// <summary>
    /// 미해금 스킨 버튼 눌렀을 때
    /// </summary>
    /// <param name="clickedBtn"></param>
    public void OnClickLockedBtn(Button clickedBtn)
    {
        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Locked.Length; i++)
        {
            if (btns_Locked[i] == clickedBtn)
            {
                buy_SelectedNumber = i;
                break;
            }
        }
    }

    /// <summary>
    /// 해금 스킨 버튼 눌렀을 때
    /// </summary>
    /// <param name="clickedBtn"></param>
    public void OnClickUnlockedBtn(Button clickedBtn)
    {
        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Unlocked.Length; i++)
        {
            if (btns_Unlocked[i] == clickedBtn)
            {
                wear_SelectedNumber = i;
                break;
            }
        }        
    }
    
    /// <summary>
    /// 구매하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickBuyYes(int selectedNumber)
    {
        // 구매 가능한 만큼 코인 보유 확인 및 차감
        bool ableToBuy = CoinManager.TryRemoveCoin(SkinManager.Inst.skinInShops[selectedNumber].skinInfo._skinPrice);
        ClosePopup();

        if(ableToBuy)
        {
            // lock 버튼 삭제
            SkinManager.Inst.skinInShops[selectedNumber].IsSkinUnlocked();
            Destroy(SkinManager.Inst.skinInShops[selectedNumber].btn_Lock);

            // PlayerPrefs에 스킨 해금 정보 저장
            PlayerPrefs.SetInt(SkinManager.Inst.skinInShops[selectedNumber].skinInfo._skinID.ToString(), 1);
            PlayerPrefs.Save();

            // 구매 완료 팝업 활성화
            openPopup = OpenPopup(popUp_Purchase, delayTime);
            StartCoroutine(openPopup);

            // txt_State.text = "보유 중";
            SkinManager.Inst.skinInShops[selectedNumber].ChangeStateText(SkinManager.Inst.skinInShops[selectedNumber].isSkinWorn);
        }
        else
        {
            // 코인 부족 팝업 활성화
            openPopup = OpenPopup(popUp_Caution, delayTime);
            StartCoroutine(openPopup);
        }
    }

    /// <summary>
    /// 착용하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickWearYes(int selectedNumber)
    {
        SkinManager.Inst.isNowDefault = false;
        TakeOffSkin();
        
        if(SkinManager.Inst.skinInShops[selectedNumber].IsSkinUnlocked())
        {
            // 선택된 스킨 적용
            SkinManager.Inst.EquipSkin(SkinManager.Inst.skinInShops[selectedNumber].skinInfo);
            SkinManager.Inst.skinInShops[selectedNumber].isSkinWorn = true;            
            SkinManager.Inst.skinInShops[selectedNumber].ChangeStateText(SkinManager.Inst.skinInShops[selectedNumber].isSkinWorn);
            ClosePopup();
        }
        else
        {
            ClosePopup();
            return;
        }
    }
    /// <summary>
    /// 착용하시겠습니까 팝업에서 no 버튼 클릭 시
    /// </summary>
    public void OnClickWearNo(int selectedNumber)
    {
        // 미리보기 원상 복귀
        SkinManager.Inst.img_Preview.sprite = Skin.lastUsedSkin.skinInfo._skinSprite;
        SkinManager.Inst.skinInShops[selectedNumber].isSkinWorn = false;

        SkinManager.Inst.skinInShops[selectedNumber].ChangeStateText(SkinManager.Inst.skinInShops[selectedNumber].isSkinWorn);
    }

    /// <summary>
    /// 다른 코스튬 버튼에서 착용 해제 효과
    /// </summary>
    public void TakeOffSkin()
    {
        foreach (SkinInShop skinInShop in SkinManager.Inst.skinInShops)
        {
            bool isNotWorn = skinInShop != SkinManager.Inst.skinInShops[wear_SelectedNumber];

            if (skinInShop.IsSkinUnlocked() && isNotWorn)
            {
                skinInShop.isSkinWorn = false;
                skinInShop.ChangeStateText(skinInShop.isSkinWorn);
            }
        }
    }

    public void TakeOffSkin(bool _isNowPreviewing)
    {
        foreach (SkinInShop skinInShop in SkinManager.Inst.skinInShops)
        {
            bool isNotWorn = skinInShop != SkinManager.Inst.skinInShops[wear_SelectedNumber] && skinInShop.skinInfo != Skin.lastUsedSkin.skinInfo;

            if (skinInShop.IsSkinUnlocked() && isNotWorn)
            {
                skinInShop.isSkinWorn = false;
                skinInShop.ChangeStateText(skinInShop.isSkinWorn);
            }
        }
    }
}