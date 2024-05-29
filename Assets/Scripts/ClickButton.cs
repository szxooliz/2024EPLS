using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClickButton : MonoBehaviour
{
    public void OnClickGameStart()
    {
        SceneManager.LoadScene("PlayScene 2");
    }

    public void OnClickCostumeRoom()
    {
        SceneManager.LoadScene("CostumeScene");
    }
    public void OnClickBack()
    {
        SceneManager.LoadScene("StartScene");
    }

    //¿Ã»ø¡÷ ª¿‘
    public void OnClickReplay()
    {
        SceneManager.LoadScene("PlayScene 1");
    }
    //ª¿‘ ≥°
}
