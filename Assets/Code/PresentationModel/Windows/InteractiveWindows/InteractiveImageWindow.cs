using Code.PresentationModel.Windows.WindowsAnimation;
using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel.Windows.InteractiveWindows
{
    [RequireComponent(typeof(VerticalWindowAnimation))]
    public class InteractiveImageWindow : InteractiveObjectWindow, IImageWindow
    {
        [SerializeField] private Image _interactiveImage;
        
        public void SetImage(Sprite image) => 
            _interactiveImage.sprite = image;
    }
}