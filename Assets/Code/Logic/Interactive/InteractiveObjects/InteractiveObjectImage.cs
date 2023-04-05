using Code.UI;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectImage : Interactivity
    {
        [SerializeField] private Sprite _sprite;
        private IImageWindow _presentationWindow;

        private bool _isWindowNull;

        [Inject]
        private void Construct(HUD hud)
        {
            hud.InteractiveImageWindow.TryGetComponent(out _presentationWindow);

            _isWindowNull = _presentationWindow == null;
        }

        public override void StartInteractive()
        {
            if (_isWindowNull)
                return;

            _presentationWindow.SetImage(_sprite);
            _presentationWindow.ShowWindow();
            OnStartInteractive?.Invoke();
        }

        public override void StopInteractive()
        {
            if (_isWindowNull)
                return;

            _presentationWindow.HideWindow();
        }
    }
}