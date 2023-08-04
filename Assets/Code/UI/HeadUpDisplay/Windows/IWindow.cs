using System;
using Code.UI.HeadUpDisplay.Elements.Buttons;
using Code.UI.HeadUpDisplay.Windows.InteractiveWindows.DialogueWindows;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows
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

   
}