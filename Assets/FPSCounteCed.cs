using System.Globalization;
using UnityEngine;

namespace RedDeck.Utilities
{
    public class FPSCounteCed : MonoBehaviour
    {
        private float _accum;
        private int _frames;
        private float _timeleft;
        private float _fps;
        private readonly float _updateInterval = 0.1f;
        private GUIStyle _textStyle = new();

        private void OnGUI()
        {
            // Розміри текстової мітки
            float labelWidth = 100;
            float labelHeight = 25;

            // Позиція для верхнього тексту, розташованого над першим текстом на висоту labelHeight
            float xPosition = (Screen.width - labelWidth) / 2;
            float yPosition = Screen.height - 2 * labelHeight - 10; // Додаткове зміщення для верхнього тексту

            // Відображення тексту над попереднім
            GUI.Label(new Rect(xPosition, yPosition, labelWidth, labelHeight), _fps.ToString("0", CultureInfo.InvariantCulture) + " FPS", _textStyle);
        }

        private void Start()
        {
            _textStyle.fontStyle = FontStyle.Bold;
            _textStyle.fontSize = 25;
            _textStyle.normal.textColor = Color.white;
            _timeleft = _updateInterval;
        }

        private void FPSCounterBehaviour()
        {
            _timeleft -= Time.deltaTime;
            _accum += Time.timeScale / Time.deltaTime;
            ++_frames;

            if (_timeleft <= 0)
            {
                _fps = (_accum / _frames);
                _timeleft = _updateInterval;
                _accum = 0;
                _frames = 0;
            }
        }


        private void Update()
        {
            if (Time.timeScale < 0.99f)
            {
                return;
            }

            FPSCounterBehaviour();
        }
    }
}


