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
        playerSR.sprite = SkinManager.equippedSkin;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
