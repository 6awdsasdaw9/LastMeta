using System;
using Code.UI.HeadUpDisplay.HudElements;
using Code.UI.HeadUpDisplay.HudElements.Buttons;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Settings;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements
{
    public class MenuWindow : HudElement, IWindow
    {
        public HudButton HudButton => _hudButton;
        [SerializeField] private HudButton _hudButton;
        public HudButton CloseButton => _closeWindowButton;
        [SerializeField] private HudButton _closeWindowButton;
        
        public HeroPanel Hero => hero;
        [Space,Title("FOLDERS")]
        [SerializeField] private HeroPanel hero;
        public SettingsWindow Settings => settings;
        [SerializeField] private SettingsWindow settings;
        
        public void ShowWindow(Action WindowShowed = null)
        {
            base.Show();
            WindowShowed?.Invoke();
        }

        public void HideWindow(Action WindowHidden = null)
        {
            base.Hide();
            WindowHidden?.Invoke();
        }

    }
}