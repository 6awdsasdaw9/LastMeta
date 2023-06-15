using System;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.Windows.DialogueWindows;
using UnityEngine;

namespace Code.PresentationModel.Windows
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

        HudButton CloseDefaultButton { get; }
    }

    public interface IHudWindow: IWindow
    {
        HudButton HudButton { get; }
        HudButton CloseButton { get; }
    }
}