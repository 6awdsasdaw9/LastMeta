using System;
using Code.PresentationModel.Buttons;
using Code.PresentationModel.Windows.MenuWindow;

namespace Code.PresentationModel.HudElements.HudButtonWindows
{
    [Serializable]
    public class MenuButtonWindow
    {
        public HudButton CloseButton;
        public HudButton Button;
        public MenuWindow Window;
    }
}