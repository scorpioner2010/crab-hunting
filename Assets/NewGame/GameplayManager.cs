using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager In;
    public Joystick joystickInput;
    
    public Button buttonAttack;

    private void Awake()
    {
        In = this;
    }
}
