using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectImage :MonoBehaviour, IInteractive
    {
        [SerializeField] private Sprite _sprite;
        private HUD _hud;

        [Inject]
        private void Construct(HUD hud)
        {
            _hud = hud;
        }

        public  void StartInteractive()
        {
            _hud.InteractiveImage.ShowInteractiveImage(_sprite);     
        }

        public  void StopInteractive()
        {
          _hud.InteractiveImage.HideInteractiveImage();
        }
    }
}