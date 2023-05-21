using System.Linq;
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
            _presentationWindow = (IImageWindow)hud.InteractiveObjectWindows
                .FirstOrDefault(w => w.Type == Type)?
                .InteractiveObjectWindow;
            
            _isWindowNull = _presentationWindow == null;
        }

        public override void StartInteractive()
        {
            if (_isWindowNull)
                return;

            OnAnimationProcess = true;
            OnStartInteractive?.Invoke();
            
            _layerAudio.PlayAudioEvent();
            _presentationWindow.SetImage(_sprite);
            _presentationWindow.ShowWindow(() => OnAnimationProcess = false);
        }

        public override void StopInteractive()
        {
            if (_isWindowNull)
                return;

            OnAnimationProcess = true;
            OnEndInteractive?.Invoke();
            
            _layerAudio.PlayAudioEvent(); 
            _presentationWindow.HideWindow(() => OnAnimationProcess = false);
        }
    }
}