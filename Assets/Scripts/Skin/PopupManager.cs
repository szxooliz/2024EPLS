using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Button[] btns_Locked;
    public Button[] btns_Unlocked;
    public SkinInShop[] skinInShops;
    public Button btn_BuyYes;
    public Button btn_WearYes;

    [SerializeField] private SkinInShop buy_SelectedSkin;
    [SerializeField] private SkinInShop wear_SelectedSkin;
    private int clickedBtnIndex;

    void Awake()
    {
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
    }

    /// <summary>
    /// 미해금 스킨 버튼 눌렀을 때
    /// </summary>
    /// <param name="clickedBtn"></param>
    public void OnClickLockedBtn(Button clickedBtn)
    {
        Debug.Log("POPUP_____" + clickedBtn.name + " was clicked!");

        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Locked.Length; i++)
        {
            if (btns_Locked[i] == clickedBtn)
            {
                clickedBtnIndex = i;
                Debug.Log("POPUP_____Clicked button index: " + clickedBtnIndex);

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
        Debug.Log("POPUP_____" + clickedBtn.name + " was clicked!");

        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Unlocked.Length; i++)
        {
            if (btns_Unlocked[i] == clickedBtn)
            {
                clickedBtnIndex = i;
                Debug.Log("POPUP_____Clicked button index: " + clickedBtnIndex);

                wear_SelectedSkin = skinInShops[i];
                break;
            }
        }

        foreach (SkinInShop skinInShop in skinInShops)
        {
            if (skinInShop != wear_SelectedSkin)
            {
                skinInShop.txt_State.text = "보유 중";
            }
        }
    }
    
    /// <summary>
    /// 구매하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickBuyYes()
    {
        Debug.Log("Click BuyYes");

        // 구매하시겠습니까 -> YES
        Debug.Log( "POPUP_____" + buy_SelectedSkin.name + " is selected");
        buy_SelectedSkin.GetComponent<SkinInShop>().OnClickBuyYes();
    
    }

    /// <summary>
    /// 착용하시겠습니까 팝업에서 yes 버튼 클릭 시
    /// </summary>
    public void OnClickWearYes()
    {
        Debug.Log("Click WearYes");
        
        Debug.Log("POPUP_____" + wear_SelectedSkin.name + " is selected");
        wear_SelectedSkin.GetComponent<SkinInShop>().OnClickWearYes();
    }
}
