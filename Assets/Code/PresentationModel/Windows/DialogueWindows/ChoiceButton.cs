using Code.PresentationModel.Buttons;
using TMPro;
using UnityEngine;

namespace Code.PresentationModel.Windows.DialogueWindows
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