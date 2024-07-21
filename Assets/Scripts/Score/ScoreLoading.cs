using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLoading : MonoBehaviour
{
    public GameObject Popup_BestScore;
    private GameObject TXT_RecordList;

    public void OnClickRecord()
    {
        TXT_RecordList = Popup_BestScore.transform.Find("TXT_RecordList").gameObject;
        // 먼저 TXT_RecordList를 비활성화
        TXT_RecordList.SetActive(false);

        // Popup_BestScore 팝업창을 활성화
        Popup_BestScore.SetActive(true);

        // 1초 후에 ActivateRecordList 메서드 호출
        Invoke("ActivateRecordList", 0.5f);
    }

    private void ActivateRecordList()
    {
        // 특정 게임 오브젝트를 활성화
        SetActiveRecursively(TXT_RecordList, true);
    }

    private void SetActiveRecursively(GameObject obj, bool state)
    {
        obj.SetActive(state);
        foreach (Transform child in obj.transform)
        {
            SetActiveRecursively(child.gameObject, state);
        }
    }
}
