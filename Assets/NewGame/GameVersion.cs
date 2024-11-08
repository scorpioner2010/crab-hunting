using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameVersion : MonoBehaviour
{
   public TMP_Text textVersion;
   private void Start()
   {
      textVersion.text = Application.version;
   }
}
