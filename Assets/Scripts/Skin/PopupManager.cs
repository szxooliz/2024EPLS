using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PopupManager : MonoBehaviour
{
    public Button[] btns_Locked;
    public SkinInShop[] skinInShops;
    public Button btn_BuyYes;
    public Button btn_WearYes;

    [SerializeField]
    private Button btn_Clicked;

    [SerializeField]
    private SkinInShop selectedSkin;
    private int clickedBtnIndex;

    void Awake()
    {
        foreach (Button btn_Locked in btns_Locked)
        {
            btn_Locked.onClick.AddListener(() => OnClickLockedBtn(btn_Locked));
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

        // 현재 선택된 스킨 기억
        btn_Clicked = clickedBtn;

        // 클릭된 버튼의 인덱스 찾기
        for (int i = 0; i < btns_Locked.Length; i++)
        {
            if (btns_Locked[i] == clickedBtn)
            {
                clickedBtnIndex = i;
                Debug.Log("POPUP_____Clicked button index: " + clickedBtnIndex);

                selectedSkin = skinInShops[i];
                break;
            }
        }
    }
    
    /// <summary>
    /// 구매하시겠습니까 팝업에서 yes 선택 시
    /// </summary>
    public void OnClickBuyYes()
    {
        Debug.Log("Click BuyYes");

        // 구매하시겠습니까 -> YES
        Debug.Log( "POPUP_____" + selectedSkin.name + " is selected");
        selectedSkin.GetComponent<SkinInShop>().OnClickBuyYes();
    
    }

    public void OnClickWearYes()
    {
        Debug.Log("Click WearYes");
        
        Debug.Log("POPUP_____" + selectedSkin.name + " is selected");
        selectedSkin.GetComponent<SkinInShop>().OnClickWearYes();
    }
}
