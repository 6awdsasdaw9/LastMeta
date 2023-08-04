using TMPro;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Elements
{
    public class TextPanel: HudElement
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.SetText(text);
        }
    }
}