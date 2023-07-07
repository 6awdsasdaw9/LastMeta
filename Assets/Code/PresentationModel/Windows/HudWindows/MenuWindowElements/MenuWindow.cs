using System;
using Code.Logic.LanguageLocalization;
using Code.PresentationModel.Buttons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Code.PresentationModel.Windows.MenuWindow
{
    public class MenuWindow : HudElement,  ILocalizationTitle, IWindow
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        public TextMeshProUGUI TitleText => _titleText;
        
        [SerializeField] private HudSlider _musicVolumeHudSlider;
        public HudSlider MusicVolumeHudSlider => _musicVolumeHudSlider;
        
        [SerializeField] private HudSlider _effectVolumeHudSlider;
        public HudSlider EffectVolumeHudSlider => _effectVolumeHudSlider;
        
        [SerializeField] private Toggle _muteToggle;
        public Toggle MuteToggle => _muteToggle;
        
        [SerializeField] private HudButton _closeWindowButton;
        public HudButton CloseButton => _closeWindowButton;
        
        [SerializeField] private HudButton _hudButton;
        public HudButton HudButton => _hudButton;
        
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