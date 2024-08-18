using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public PatternSpawn patternSpawnPoint;
    public static PatternManager sharedInstance = null;

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