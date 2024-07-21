using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BTN : MonoBehaviour
{
    public void BTN_Replay()
    {
        GameManager.Inst.OnClickReplay();
    }

    public void BTN_Main()
    {
        GameManager.Inst.OnClickBack();
    }
}
