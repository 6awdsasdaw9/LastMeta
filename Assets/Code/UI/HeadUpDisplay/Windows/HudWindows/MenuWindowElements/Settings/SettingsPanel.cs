using System;
using Code.Services.LanguageLocalization;
using Code.UI.HeadUpDisplay.HudElements;
using Code.UI.HeadUpDisplay.HudElements.Buttons;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Settings
{
    public class SettingsPanel : HudElement, ILocalizationTitle, IWindow
    {
        [SerializeField] private HudButton _folderButton;
        public HudButton FolderButton => _folderButton;
        
        [SerializeField] private TextMeshProUGUI _titleText;
        public TextMeshProUGUI TitleText => _titleText;

        [Space,Title("SOUND")]
        [SerializeField] private HudSlider _musicVolumeHudSlider;
        public HudSlider MusicVolumeHudSlider => _musicVolumeHudSlider;

        [SerializeField] private HudSlider _effectVolumeHudSlider;
        public HudSlider EffectVolumeHudSlider => _effectVolumeHudSlider;

        [SerializeField] private Toggle _muteToggle;
        public Toggle MuteToggle => _muteToggle;


        [Space,Title("LOCALIZATION")]
        [SerializeField] private HudButton _rusLanguage;
        public HudButton RusLanguage => _rusLanguage;

        [SerializeField] private HudButton _engLanguage;
        public HudButton EngLanguage => _engLanguage;

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

        public void SetTitle(string title)
        {
            _titleText.SetText(title);
        }
    }
}