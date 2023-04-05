using UnityEngine;

namespace Code.UI
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
}