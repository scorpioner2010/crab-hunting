using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static MainCamera In;
    public Transform target;
    
    private void Update()
    {
        if (target == null)
        {
            return;
        }
        
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void Awake()
    {
        In = this;
    }
}
