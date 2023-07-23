using Code.UI.HeadUpDisplay.Windows.HudElementsAnimation;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows
{
    [RequireComponent(typeof(VerticalWindowAnimation))]
    public class InteractiveImageWindow : InteractiveObjectWindow, IImageWindow
    {
        [SerializeField] private Image _interactiveImage;
        
        public void SetImage(Sprite image) => 
            _interactiveImage.sprite = image;
    }
}