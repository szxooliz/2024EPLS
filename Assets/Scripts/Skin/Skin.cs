using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skin : MonoBehaviour
{
    public static Skin Inst;
    public static SkinInShop lastUsedSkin; // 가장 마지막에 착용한 코스튬
    void Start()
    {
        if (Inst == null)
        {
            Inst = this;
        }
        else
        {
            Destroy(gameObject);
            Inst = this;
        }
        DontDestroyOnLoad(Inst);
    }
}
