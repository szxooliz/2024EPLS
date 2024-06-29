using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public static PopupManager Inst; // Singleton
    public Button[] btns_Locked;
    public Button[] btns_Unlocked;
    public SkinInShop[] skinInShops;
    public Button btn_BuyYes;
    public Button btn_WearYes;
    public Button btn_WearNo;



    [SerializeField] private SkinInShop buy_SelectedSkin;
    [SerializeField] private SkinInShop wear_SelectedSkin;

    void Awake()
    {
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

        btn_BuyYes.onClick.AddListener(OnClickBuyYes);
        btn_WearYes.onClick.AddListener(OnClickWearYes);
        btn_WearNo.onClick.AddListener(OnClickWearNo);
    }

    /// <summary>
    /// 미해금 스킨 버튼 눌렀을 때
    /// </summary>
    /// <param name="clickedBtn"></param>
    public void OnClickLockedBtn(Button clickedBtn)
    {
        //Debug.Log("POPUP_____" + clickedBtn.name + " was clicked!");

        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Locked.Length; i++)
        {
            if (btns_Locked[i] == clickedBtn)
            {
                buy_SelectedSkin = skinInShops[i];
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
        // Debug.Log("POPUP_____" + clickedBtn.name + " was clicked!");

        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Unlocked.Length; i++)
        {
            if (btns_Unlocked[i] == clickedBtn)
            {
                wear_SelectedSkin = skinInShops[i];
                break;
            }
        }        
    }
    
    /// <summary>
    /// 구매하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickBuyYes()
    {
        Debug.Log("Click BuyYes");
        buy_SelectedSkin.GetComponent<SkinInShop>().OnClickBuyYes();
    
    }

    /// <summary>
    /// 착용하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickWearYes()
    {
        Debug.Log("Click WearYes");
        
        wear_SelectedSkin.GetComponent<SkinInShop>().OnClickWearYes();

        foreach (SkinInShop skinInShop in skinInShops)
        {
            bool isNotWorn = skinInShop != wear_SelectedSkin && skinInShop.skinInfo._skinSprite != SkinManager.equippedSkin;
            bool isUnlocked = PlayerPrefs.GetInt(skinInShop.skinInfo._skinID.ToString()) == 1;
            
            if (isNotWorn && isUnlocked)
            {
                skinInShop.isSkinWorn = false;
                skinInShop.txt_State.text = "보유 중";
            }
        }
    }
    /// <summary>
    /// 착용하시겠습니까 팝업에서 no 버튼 클릭 시
    /// </summary>
    public void OnClickWearNo()
    {
        SkinManager.Inst.img_Preview.sprite = SkinManager.equippedSkin;
    }
}
