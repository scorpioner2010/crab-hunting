using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraController : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void Awake()
    {
        Application.targetFrameRate = 120;
    }
}
