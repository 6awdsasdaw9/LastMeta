using System;
using UnityEngine;

namespace Code.UI.Windows.DialogueWindows
{
    public class InteractiveDialogueWindow : InteractiveObjectWindow, IDialogueWindow
    {
        public DialogueController DialogueController => _dialogueController; 
        [SerializeField] private DialogueController _dialogueController;
        
        public virtual void ShowWindow(Action WindowShowed)
        {
            if(_animation.IsPlay)
                return;

            _animation.PlayShow(WindowShowed);
            _hud.OnUIWindowShown?.Invoke();
        }

        public virtual void HideWindow(Action WindowHidden)
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