using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPH : MonoBehaviour
{
    public Transform target;
    public Image hpBar;
    public float Hp;

    private void Update()
    {
        hpBar.fillAmount = Hp;
    }
}
