using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectImage :MonoBehaviour, IInteractive
    {
        [SerializeField] private Sprite _sprite;
        private InteractiveImageWindow _imageHud;

        [Inject]
        private void Construct(HUD hud)
        {
            _imageHud = hud.InteractiveImageWindow;
        }

        public  void StartInteractive()
        {
            _imageHud.SetSprite(_sprite);
            _imageHud.ShowWindow();     
        }

        public  void StopInteractive()
        {
          _imageHud.HideWindow();
        }
    }
}