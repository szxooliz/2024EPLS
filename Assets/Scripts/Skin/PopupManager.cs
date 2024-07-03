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
    // public SkinInShop[] skinInShops;

    public GameObject popUp_Buy; // 구매 재질문 팝업
    public GameObject popUp_Caution; // 코인 부족 시 경고 팝업
    public GameObject popUp_Wear; // 적용 질문 팝업
    public GameObject popUp_Purchase; // 구매 완료 팝업
    public GameObject panel; // 뒷 패널
    public TextMeshProUGUI txt_PopupPrice; // 팝업 위 코스튬 가격 텍스트

    public Button btn_BuyYes;
    public Button btn_WearYes;
    public Button btn_WearNo;

    [SerializeField] private SkinInShop buy_SelectedSkin;
    [SerializeField] private SkinInShop wear_SelectedSkin;
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

        btn_BuyYes.onClick.AddListener(() => OnClickBuyYes(buy_SelectedSkin));
        btn_WearYes.onClick.AddListener(() => OnClickWearYes(wear_SelectedSkin));
        btn_WearNo.onClick.AddListener(OnClickWearNo);
    }

    /// <summary>
    /// 팝업 열기
    /// </summary>
    public IEnumerator OpenPopup(GameObject popUp, float delay)
    {
        Debug.Log("PopupManager --- OpenPopup start");
        popUp_Active = popUp;
        popUp_Active.SetActive(true);
        panel.SetActive(true);

        yield return new WaitForSeconds(delay);
        Debug.Log("PopupManager --- OpenPopup finish");
    }

    /// <summary>
    /// 팝업 닫기
    /// </summary>
    public void ClosePopup()
    {
        // 미리보기 원상 복귀
        // SkinManager.Inst.img_Preview.sprite = SkinManager.equippedSkin;
        SkinManager.Inst.img_Preview.sprite = SkinManager.lastUsedSkin.skinInfo._skinSprite;

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
                buy_SelectedSkin = SkinManager.Inst.skinInShops[i];
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
                wear_SelectedSkin = SkinManager.Inst.skinInShops[i];
                break;
            }
        }        
    }
    
    /// <summary>
    /// 구매하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickBuyYes(SkinInShop _skinInShop)
    {
        Debug.Log("Click BuyYes");

        // buy_SelectedSkin.GetComponent<SkinInShop>().OnClickBuyYes();
        // 구매 가능한 만큼 코인 보유 확인 및 차감
        bool ableToBuy = CoinManager.TryRemoveCoin(buy_SelectedSkin.skinInfo._skinPrice);
        ClosePopup();

        if(ableToBuy)
        {
            // lock 버튼 삭제
            _skinInShop.IsSkinUnlocked();
            Destroy(_skinInShop.btn_Lock);

            // PlayerPrefs에 스킨 해금 정보 저장
            PlayerPrefs.SetInt(_skinInShop.skinInfo._skinID.ToString(), 1);
            PlayerPrefs.Save();

            // 구매 완료 팝업 활성화
            openPopup = OpenPopup(popUp_Purchase, delayTime);
            StartCoroutine(openPopup);
            // Invoke("OpenPopup", delayTime); 

            // txt_State.text = "보유 중";
            _skinInShop.ChangeStateText(_skinInShop.isSkinWorn);
        }
        else
        {
            // 코인 부족 팝업 활성화
            openPopup = OpenPopup(popUp_Caution, delayTime);
            StartCoroutine(openPopup);
            // Invoke("OpenPopup", delayTime); 
        }
    }

    /// <summary>
    /// 착용하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickWearYes(SkinInShop _skinInShop)
    {
        Debug.Log("Click WearYes");
        
        // wear_SelectedSkin.GetComponent<SkinInShop>().OnClickWearYes();
        if(_skinInShop.IsSkinUnlocked())
        {
            // 선택된 스킨 적용
            SkinManager.Inst.EquipSkin(_skinInShop.skinInfo);
            _skinInShop.isSkinWorn = true;

            Debug.Log("SKININSHOP____" + _skinInShop.skinInfo._skinName + " 착용 중 텍스트!");
            
            // txt_State.text = "착용 중"; 
            _skinInShop.ChangeStateText(_skinInShop.isSkinWorn);
            ClosePopup();
        }
        else
        {
            ClosePopup();
            return;
        }

        SkinManager.Inst.isNowDefault = false;

        // 다른 코스튬 버튼에서 착용 해제 효과
        foreach (SkinInShop skinInShop in SkinManager.Inst.skinInShops)
        {
            // bool isNotWorn = skinInShop != wear_SelectedSkin && skinInShop.skinInfo._skinSprite != SkinManager.equippedSkin;
            bool isNotWorn = skinInShop != wear_SelectedSkin && skinInShop.skinInfo != SkinManager.lastUsedSkin.skinInfo;
            bool isUnlocked = PlayerPrefs.GetInt(skinInShop.skinInfo._skinID.ToString()) == 1;
            
            if (isNotWorn && isUnlocked)
            {
                skinInShop.isSkinWorn = false;
                // skinInShop.txt_State.text = "보유 중";
                skinInShop.ChangeStateText(skinInShop.isSkinWorn);
            }
        }
    }
    /// <summary>
    /// 착용하시겠습니까 팝업에서 no 버튼 클릭 시
    /// </summary>
    public void OnClickWearNo()
    {
        // 미리보기 원상 복귀
        // SkinManager.Inst.img_Preview.sprite = SkinManager.equippedSkin;
         SkinManager.Inst.img_Preview.sprite = SkinManager.lastUsedSkin.skinInfo._skinSprite;
        wear_SelectedSkin.isSkinWorn = false;

        // wear_SelectedSkin.txt_State.text = "보유 중";
        wear_SelectedSkin.ChangeStateText(wear_SelectedSkin.isSkinWorn);
    }
}
