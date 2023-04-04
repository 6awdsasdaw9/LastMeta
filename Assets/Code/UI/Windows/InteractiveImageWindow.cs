using Code.UI.Windows;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI
{
    [RequireComponent(typeof(InteractiveWindowAnimation))]
    public class InteractiveImageWindow : InteractiveObjectWindow
    {
        [SerializeField] private Image _interactiveImage;
        
        public void SetSprite(Sprite sprite) => 
            _interactiveImage.sprite = sprite;
        
    }
}