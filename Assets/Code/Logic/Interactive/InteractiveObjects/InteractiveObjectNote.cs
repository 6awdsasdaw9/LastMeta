using System.Linq;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.UI;
using Code.UI.Windows;
using Ink.Runtime;
using UnityEngine;
using Zenject;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectNote : Interactivity
    {
        [SerializeField] private int _id;
        
        private Sprite _noteImage;
        private TextAsset _inkJSON;
      
        
        private INoteWindow _presentationWindow;

        private bool _isNull;

        [Inject]
        private void Construct(HUD hud,TextConfig textConfig)
        {
            hud.InteractiveNoteWindow.TryGetComponent(out _presentationWindow);

            _inkJSON = textConfig.Notes.FirstOrDefault(n => n.Id == _id)!.inkJSON;
            _noteImage = textConfig.Notes.FirstOrDefault(n => n.Id == _id)!.NoteImage;
            
            _isNull = _presentationWindow == null || _inkJSON == null;
        }

        public override void StartInteractive()
        {
            if (_isNull)
                return;

            var message = new Story(_inkJSON.text).ContinueMaximally();
            
            _presentationWindow.SetText(message);
            _presentationWindow.SetImage(_noteImage);
            
            _presentationWindow.ShowWindow();
            OnStartInteractive?.Invoke();
        }

        public override void StopInteractive()
        {
            if (_isNull)
                return;

            _presentationWindow.HideWindow();
            OnEndInteractive?.Invoke();
        }
    }
}