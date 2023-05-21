using Code.UI.Buttons;
using TMPro;
using UnityEngine;

namespace Code.Logic.UI.Windows.DialogueWindows
{
    public class ChoiceButton:ButtonTap
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string text)
        {
            _text.SetText(text);
        }
    }
}