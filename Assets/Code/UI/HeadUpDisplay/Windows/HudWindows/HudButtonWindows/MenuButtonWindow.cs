using System;
using Code.UI.HeadUpDisplay.Elements.Buttons;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.HudButtonWindows
{
    [Serializable]
    public class MenuButtonWindow
    {
        public HudButton CloseButton;
        public HudButton Button;
        public MenuWindow Window;
    }
}