using Code.UI.HeadUpDisplay.Windows.HudElementsAnimation;
using TMPro;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows
{
    [RequireComponent(typeof(VerticalWindowAnimation))]
    public class InteractiveNoteWindow: InteractiveImageWindow, INoteWindow
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string message) => 
            _text.SetText(message);
        
    }
}