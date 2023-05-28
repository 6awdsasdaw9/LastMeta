using System;
using Code.UI.Buttons;
using Code.UI.Windows.DialogueWindows;
using UnityEngine;

namespace Code.UI.Windows
{
    public interface IWindow
    {
        void ShowWindow(Action WindowShowed = null);
        void HideWindow(Action WindowHidden = null);
    }

    public interface IImageWindow : IWindow
    {
        void SetImage(Sprite image);
    }

    public interface INoteWindow : IImageWindow
    {
        void SetText(string message);
    }

    public interface IDialogueWindow : IWindow
    {
        DialogueController DialogueController { get; }

        ButtonTap CloseButton { get; }
        ButtonTap SkipButton { get; }
    }
}