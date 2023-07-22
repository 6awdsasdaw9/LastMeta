using System.Linq;
using Code.Audio.AudioEvents;
using Code.PresentationModel.HeadUpDisplay;
using Code.PresentationModel.Windows;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.Interactive.InteractiveObjects
{
    public class HUDInteractiveObject : Interactivity
    {
        [SerializeField] private AudioEvent _layerAudioEvent;

        private IWindow _presentationWindow;
        private bool _isWindowNull;

        [Inject]
        private void Construct(Hud hud)
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
            OnStopInteractive?.Invoke();
            
            _layerAudioEvent.PlayAudioEvent();
            _presentationWindow.HideWindow(() => OnAnimationProcess = false);
        }
    }
}