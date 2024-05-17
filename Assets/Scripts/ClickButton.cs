using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainButton : MonoBehaviour
{
    public void OnClickGameStart()
    {
        SceneManager.LoadScene("PlayScene");
    }

    public void OnClickCostumeRoom()
    {
        SceneManager.LoadScene("CostumeScene");
    }
    public void OnClickSettings()
    {

    }

    public void OnClickBack()
    {
        SceneManager.LoadScene("StartScene");
    }

}
