using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject[] inputButtons;
    public Button play;
    public NetworkManager manager;
    public GameObject mainMenu;
    
    private void Start()
    {
        foreach (var button in inputButtons)
        {
            button.SetActive(false);
        }
        
        play.onClick.AddListener(() =>
        {
            foreach (var button in inputButtons)
            {
                button.SetActive(true);
            }
            
            mainMenu.SetActive(false);
            
            NetworkServer.dontListen = true;
            manager.StartHost();
        });
    }
}
