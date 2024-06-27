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
    }

    public void OnClickCostumeRoom()
    {
        SceneManager.LoadScene("CostumeScene");
        AudioManager.Instance.PlaySFX("Button_UI");
    }
    public void OnClickBack()
    {
        SceneManager.LoadScene("StartScene");
    }

    //��ȿ�� ����
    public void OnClickReplay()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("PlayScene 1");
    }
    //���� ��
}
