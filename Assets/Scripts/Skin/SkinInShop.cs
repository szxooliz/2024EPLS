using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinInShop : MonoBehaviour
{
    public SO_SkinInfo skinInfo;
    public TextMeshProUGUI stateText; // 장착 or 보유 상태 표시 텍스트
    public TextMeshProUGUI priceText; // 코스튬 가격 텍스트
    public Image skinImage; // 버튼 위 보여지는 코스튬
    public Image previewImage; // 미리보기 코스튬 파츠
    public GameObject lockBtn; // 해금 전 회색 반투명 레이어
    public GameObject buyPopUp; // 구매 재질문 팝업
    public GameObject cautionPopUp; // 코인 부족 시 경고 팝업
    public GameObject wearPopUp; // 적용 질문 팝업
    public bool isSkinUnlocked = false; // 해금 여부

    private void Awake() 
    {
        // 안 되는 것 같음.. 나중에 다시 확인
        // previewImage = GameObject.Find("IMG_PlayerPreview").GetComponent<Image>();
        // cautionPopUp = GameObject.Find("PopUp_Caution");
        
        skinImage.sprite = skinInfo.skinSprite;
        //IsSkinUnlocked();
    }

    /// <summary>
    /// 코스튬 잠금 풀기
    /// </summary>
    public void IsSkinUnlocked()
    {
        // 여기 뭐지? 코드 다시 살펴보기
        if (PlayerPrefs.GetInt(skinInfo.skinID.ToString()) == 1)
        {
            isSkinUnlocked = true;
            stateText.text = "bought";
        }
    }
    /// <summary>
    /// 미해금 코스튬 버튼 눌렀을 시
    /// </summary>
    public void OnClickTryBuy()
    {
        if(isSkinUnlocked)
        {
            Debug.Log("isSkinUnlocked: " + isSkinUnlocked.ToString());
            Debug.Log("Skin price: " + skinInfo.skinPrice.ToString());
            // 이미 구매 했을 시
            Destroy(lockBtn);
        }
        else
        {
            Debug.Log("isSkinUnlocked: " + isSkinUnlocked.ToString());
            Debug.Log("Skin price: " + skinInfo.skinPrice.ToString());
            buyPopUp.SetActive(true);
            priceText.text = skinInfo.skinPrice.ToString();
        }
    }

    /// <summary>
    /// 구매하시겠습니까? 팝업에서 yes 선택 시
    /// </summary>
    public void OnClickBuyYes()
    {
        // 구매 가능한 만큼 코인 보유 확인
        bool ableToBuy = PlayerCoin.TryRemoveCoin(skinInfo.skinPrice);
        Debug.Log("ableToBuy: " + ableToBuy.ToString());
        if(ableToBuy)
        {
            IsSkinUnlocked();
            Destroy(lockBtn);
        }
        else
        {
            cautionPopUp.SetActive(true);
        } 
    }

    /// <summary>
    /// 해금한 코스튬 적용
    /// </summary>
    public void OnClickWear()
    {
        previewImage.sprite = skinInfo.skinSprite;
        if(isSkinUnlocked)
        {
            // equip skin
            FindObjectOfType<SkinManager>().EquipSkin(skinInfo);
            
            wearPopUp.SetActive(true);
            //stateText.text = "Equiped";
        }
        else
        {
            // 예외 처리 해야 됨
            return;
        }

    }
    // public void OnButtonPress()
    // {
    //     previewImage.sprite = skinInfo.skinSprite;
    //     if(isSkinUnlocked)
    //     {
    //         // equip skin
    //         FindObjectOfType<SkinManager>().EquipSkin(skinInfo);
    //     }
    //     else
    //     {
    //         // buy skin
    //         if (PlayerCoin.TryRemoveCoin(skinInfo.skinPrice))
    //         {
    //             PlayerPrefs.SetInt(skinInfo.skinID.ToString(), 1);
    //             IsSkinUnlocked();
    //         }
    //     }
    // }
}
