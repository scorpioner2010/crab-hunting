using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using Unity.VisualScripting;
using UnityEngine;

namespace RedDeck.Utilities
{
    public class FPSCounter : MonoBehaviour
    {
        private int tick;
        private float time;
        public int FPS;
        private GUIStyle _textStyle = new();

        private void OnGUI()
        {
            // Розміри текстової мітки
            float labelWidth = 100;
            float labelHeight = 25;

            // Позиція для нижнього тексту, з відступом 10 пікселів від низу
            float xPosition = (Screen.width - labelWidth) / 2;
            float yPosition = Screen.height - labelHeight - 60;

            // Відображення тексту внизу
            GUI.Label(new Rect(xPosition, yPosition, labelWidth, labelHeight), FPS.ToString("0", CultureInfo.InvariantCulture) + " FPS", _textStyle);
        }

        private void Start()
        {
            _textStyle.fontStyle = FontStyle.Bold;
            _textStyle.fontSize = 25;
            _textStyle.normal.textColor = Color.white;
        }

        private void Update()
        {
            if (Time.time >= time + 1)
            {
                time = Time.time;
                FPS = tick;
                tick = 0;
            }

            tick++;
        }
    }
}