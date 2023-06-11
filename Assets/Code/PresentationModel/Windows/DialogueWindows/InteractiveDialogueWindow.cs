using System;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.Windows.InteractiveWindows;
using UnityEngine;

namespace Code.PresentationModel.Windows.DialogueWindows
{
    public sealed class InteractiveDialogueWindow : InteractiveObjectWindow, IDialogueWindow
    {
        public DialogueController DialogueController => _dialogueController;
        [SerializeField] private DialogueController _dialogueController;
        public ButtonTap CloseButton => _buttonClose;

        [SerializeField] private ButtonTap _buttonClose;

        public override void ShowWindow(Action WindowShowed)
        {
            if (_animation.IsPlay)
                return;

            _animation.PlayShow(WindowShowed);
            _hud.OnUIWindowShown?.Invoke();
        }

        public override void HideWindow(Action WindowHidden)
        {
            if (_animation.IsPlay)
                return;

            _dialogueController.StopDialogue();

            _animation.PlayHide(WindowHidden);
            _hud.OnUIWindowHidden?.Invoke();
        }
    }
}