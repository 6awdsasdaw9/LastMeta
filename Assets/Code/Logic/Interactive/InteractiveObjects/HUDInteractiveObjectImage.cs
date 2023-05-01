using Code.UI;
using Code.UI.HeadUpDisplay;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class HUDInteractiveObjectImage : Interactivity
    {
        [SerializeField] private Sprite _sprite;
        [SerializeField] private AudioEvent _layerAudio;
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

            OnProcess = true;
            OnStartInteractive?.Invoke();
            
            _layerAudio.PlayAudioEvent();
            _presentationWindow.SetImage(_sprite);
            _presentationWindow.ShowWindow(() => OnProcess = false);
        }

        public override void StopInteractive()
        {
            if (_isWindowNull)
                return;

            OnProcess = true;
            OnEndInteractive?.Invoke();
            
            _layerAudio.PlayAudioEvent(); 
            _presentationWindow.HideWindow(() => OnProcess = false);
        }
    }
}