using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.Windows
{
    [RequireComponent(typeof(InteractiveWindowAnimation))]
    public class InteractiveImageWindow : InteractiveObjectWindow, IImageWindow
    {
        [SerializeField] private Image _interactiveImage;
        
        public void SetImage(Sprite image) => 
            _interactiveImage.sprite = image;
    }
}