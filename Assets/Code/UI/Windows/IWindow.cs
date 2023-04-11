using UnityEngine;

namespace Code.UI.Windows
{
    public interface IWindow
    {
        void ShowWindow();
        void HideWindow();
    }

    public interface IImageWindow : IWindow
    {
        void SetImage(Sprite image);
    }

    public interface INoteWindow : IImageWindow
    {
        void SetText(string message);
    }
}