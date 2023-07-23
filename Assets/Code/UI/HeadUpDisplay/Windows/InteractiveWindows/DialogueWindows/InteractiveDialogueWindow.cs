using System;
using Code.UI.HeadUpDisplay.HudElements.Buttons;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.InteractiveWindows.DialogueWindows
{
    public sealed class InteractiveDialogueWindow : InteractiveObjectWindow, IDialogueWindow
    {
        public DialogueController DialogueController => _dialogueController;
        [SerializeField] private DialogueController _dialogueController;
        public HudButton CloseDefaultButton => defaultButtonClose;
        [SerializeField] private HudButton defaultButtonClose;

        public override void ShowWindow(Action WindowShowed)
        {
            if (_animation.IsPlay)
                return;

            _animation.PlayShow(WindowShowed);
        }

        public override void HideWindow(Action WindowHidden)
        {
            if (_animation.IsPlay)
                return;

            _dialogueController.StopDialogue();

            _animation.PlayHide(WindowHidden);
        }
    }
}