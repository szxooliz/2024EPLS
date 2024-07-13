using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatternManager : MonoBehaviour
{
    public SpawnPoint patternSpawnPoint;
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

    private void Start()
    {
        SetUpScene();
    }

    public void SetUpScene()
    {
        SpawnPattern();
    }

    public void SpawnPattern()
    {
        if(patternSpawnPoint != null)
        {
            GameObject pattern = patternSpawnPoint.SpawnObject();
        }
    }
}
