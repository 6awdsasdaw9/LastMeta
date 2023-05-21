using System.Linq;
using Code.UI.HeadUpDisplay;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class HUDInteractiveObject : Interactivity
    {
        [SerializeField] private AudioEvent _layerAudioEvent;

        private IWindow _presentationWindow;
        private bool _isWindowNull;

        [Inject]
        private void Construct(HUD hud)
        {
            hud.InteractiveObjectWindows
                .FirstOrDefault(w => w.Type == Type)?
                .InteractiveObjectWindow
                .TryGetComponent(out _presentationWindow);

            _isWindowNull = _presentationWindow == null;
        }

        public override void StartInteractive()
        {
            if (_isWindowNull)
                return;

            OnAnimationProcess = true;
            OnStartInteractive?.Invoke();
            
            _layerAudioEvent.PlayAudioEvent();
            _presentationWindow.ShowWindow(() => OnAnimationProcess = false);
        }

        public override void StopInteractive()
        {
            if (_isWindowNull)
                return;

            OnAnimationProcess = true;
            OnEndInteractive?.Invoke();
            
            _layerAudioEvent.PlayAudioEvent();
            _presentationWindow.HideWindow(() => OnAnimationProcess = false);
        }
    }
}