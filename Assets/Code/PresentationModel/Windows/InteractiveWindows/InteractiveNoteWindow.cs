using Code.PresentationModel.Windows.WindowsAnimation;
using TMPro;
using UnityEngine;

namespace Code.PresentationModel.Windows.InteractiveWindows
{
    [RequireComponent(typeof(VerticalWindowAnimation))]
    public class InteractiveNoteWindow: InteractiveImageWindow, INoteWindow
    {
        [SerializeField] private TextMeshProUGUI _text;

        public void SetText(string message) => 
            _text.SetText(message);
        
    }
}