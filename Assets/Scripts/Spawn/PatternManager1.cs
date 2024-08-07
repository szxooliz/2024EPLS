using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _PatternManager : MonoBehaviour
{
    public PatternSpawn patternSpawnPoint;
    public static _PatternManager sharedInstance = null;

    private void Awake()
    {
        if(sharedInstance != null && sharedInstance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            sharedInstance = this;
        }
    }
}