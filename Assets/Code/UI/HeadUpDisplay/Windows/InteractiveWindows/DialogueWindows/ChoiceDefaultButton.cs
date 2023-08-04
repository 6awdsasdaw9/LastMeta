using Code.UI.HeadUpDisplay.Elements.Buttons;
using TMPro;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows.DialogueWindows
{
    public class ChoiceDefaultButton:HudButton
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.SetText(text);
        }
    }
}