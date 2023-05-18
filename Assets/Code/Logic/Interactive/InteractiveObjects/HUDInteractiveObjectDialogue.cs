using System.Linq;
using Code.Debugers;
using Code.UI.HeadUpDisplay;
using Code.UI.Windows;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class HUDInteractiveObjectDialogue : Interactivity
    {
        [SerializeField] private TextAsset _textAsset;
        [SerializeField] private AudioEvent _layerAudio;
        private IDialogueWindow _presentationWindow;

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

            Log.ColorLog("Start interactive");
            OnProcess = true;
            OnStartInteractive?.Invoke();
            
            _layerAudio.PlayAudioEvent();
            _presentationWindow.ShowWindow(WindowShowed: WindowShowed);
        }

        private void WindowShowed()
        {
             OnProcess = false;
            _presentationWindow.DialogueController.StartStory(_textAsset);
        }

        public override void StopInteractive()
        {
            if (_isWindowNull)
                return;

            OnProcess = true;
            OnEndInteractive?.Invoke();
            
            _layerAudio.PlayAudioEvent(); 
            //TODO stopDialogue
            _presentationWindow.HideWindow(() => OnProcess = false);
        }
    }
}