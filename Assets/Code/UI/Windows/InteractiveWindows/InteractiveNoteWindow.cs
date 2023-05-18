using Code.UI.Windows.Animation;
using TMPro;
using UnityEngine;

namespace Code.UI.Windows
{
    [RequireComponent(typeof(VerticalWindowAnimation))]
    public class InteractiveNoteWindow: InteractiveImageWindow, INoteWindow
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string message) => 
            _text.SetText(message);
        
    }
}