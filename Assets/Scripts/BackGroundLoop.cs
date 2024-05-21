using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    [SerializeField] private float speed = 4f;

    private float backgroundWidth;

<<<<<<< HEAD
    private Vector3 startPosition;
=======
    private new Vector3 startPosition;
>>>>>>> 5810174e85b8de407e334a3a57d68b9216db6afa
    
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        backgroundWidth = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * speed, backgroundWidth);
        transform.position = startPosition + Vector3.left * newPosition;
    }
}
