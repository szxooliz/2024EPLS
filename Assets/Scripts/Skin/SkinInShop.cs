using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SkinInShop : MonoBehaviour
{
    public SO_SkinInfo skinInfo;
    public TextMeshProUGUI stateText; // 장착 or 보유 상태 표시 텍스트
    public Image skinImage; // 버튼 위 보여지는 코스튬
    public Image previewImage; // 코스튬 파츠
    public GameObject _lock; // 해금 전 회색 반투명 레이어
    public GameObject _caution; // 코인 부족 시 경고 팝업
    public bool isSkinUnlocked; // 해금 여부

    private void Awake() 
    {
        // 오류메세지 다음에 또 뜨는지 확인
        skinImage.sprite = skinInfo.skinSprite;
        IsSkinUnlocked();
    }

    /// <summary>
    /// 코스튬 해금
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
    /// 구매하시겠습니까? 팝업에서 yes 선택 시
    /// </summary>
    public void OnClickBuyYes()
    {
        // 구매 가능한 만큼 코인 보유 확인
        bool ableToBuy = PlayerCoin.TryRemoveCoin(skinInfo.skinPrice);

        if(ableToBuy)
        {
            IsSkinUnlocked();
            Destroy(_lock);
        }
        else
        {
            _caution.gameObject.SetActive(true);
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
            stateText.text = "Equiped";
        }
        else
        {
            // 예외 처리
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
