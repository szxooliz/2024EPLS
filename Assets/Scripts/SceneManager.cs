using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    public void OnClickGameStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }

    public void OnClickCostumeRoom()
    {
        SceneManager.LoadScene("CostumeScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }
    public void OnClickBack()
    {
        Debug.Log("버튼아 눌려라 얍!");
        SceneManager.LoadScene("StartScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }

    public void OnClickReplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }

    /// <summary>
    /// 테스트용 코드
    /// </summary>
    public void OnClickReplay3()
    {
        Debug.Log("버튼아 눌려라 얍!");
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene 3");
        AudioManager.Instance.PlaySFX("Button_UI");
    }
}
