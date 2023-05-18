using System;
using Code.UI.HeadUpDisplay;
using Code.UI.Windows.Animation;
using UnityEngine;

namespace Code.UI.Windows.DialogueWindows
{
    public class InteractiveDialogueWindow : InteractiveObjectWindow, IDialogueWindow
    {
        [SerializeField] private HUD _hud;
        [SerializeField] private WindowAnimation _animation;
        [SerializeField] private DialogueController _dialogueController;
        
        public DialogueController DialogueController => _dialogueController; 
        
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
      
            _animation.PlayHide(WindowHidden);
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}