using System;
using Code.UI.Buttons;
using UnityEngine;

namespace Code.UI.Windows.DialogueWindows
{
    public sealed class InteractiveDialogueWindow : InteractiveObjectWindow, IDialogueWindow
    {
        public DialogueController DialogueController => _dialogueController;
        [SerializeField] private DialogueController _dialogueController;
        public ButtonTap CloseButton => _buttonClose;
        [SerializeField] private ButtonTap _buttonClose;
        
       

        public override void ShowWindow(Action WindowShowed )
        {
            if(_animation.IsPlay)
                return;

            _animation.PlayShow(WindowShowed);
            _hud.OnUIWindowShown?.Invoke();
        }

        public override void HideWindow(Action WindowHidden)
        {
            if(_animation.IsPlay)
                return;
            
            //TODO StopStory
            _dialogueController.StopDialogue();
      
            _animation.PlayHide(WindowHidden);
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}