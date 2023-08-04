using System;
using Code.Services.LanguageLocalization;
using Code.UI.HeadUpDisplay.Elements;
using Code.UI.HeadUpDisplay.Elements.Buttons;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero.ArtifactsElements;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero.HeroParamsElements;
using TMPro;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero
{
    public class HeroPanel : HudElement,  ILocalizationTitle, IWindow
    {
        [SerializeField] private TextMeshProUGUI _titleText;
        public ArtifactsPanel ArtifactsPanel => _artifactsPanel;
        [SerializeField] private ArtifactsPanel _artifactsPanel;
        public HeroParamPanel HeroParamPanel => _heroParamPanel;
        [SerializeField] private HeroParamPanel _heroParamPanel;
        public HudButton FolderButton => _folderButton;
        [SerializeField] private HudButton _folderButton;

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