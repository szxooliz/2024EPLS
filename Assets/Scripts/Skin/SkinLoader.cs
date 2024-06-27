using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinLoader : MonoBehaviour
{
    public SpriteRenderer playerSR;
    void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        // 애니메이션 지정..
        playerSR.sprite = SkinManager.equippedSkin; // 이건 나중에 삭제
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
