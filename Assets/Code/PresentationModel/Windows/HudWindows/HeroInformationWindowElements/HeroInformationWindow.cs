using System;
using Code.Logic.LanguageLocalization;
using Code.PresentationModel.Buttons;
using TMPro;
using UnityEngine;

namespace Code.PresentationModel.Windows.HudWindows.HeroInformationWindowElements
{
    public class HeroInformationWindow : HudElement,  ILocalizationTitle
    {
        [SerializeField] private TextMeshProUGUI _titleText;

        public ArtifactsPanel ArtifactsPanel => _artifactsPanel;
        [SerializeField] private ArtifactsPanel _artifactsPanel;

        public HeroParamPanel HeroParamPanel => _heroParamPanel;
        [SerializeField] private HeroParamPanel _heroParamPanel;
        

        public void ShowWindow(Action WindowShowed = null)
        {
            base.Show();
        }

        public void HideWindow(Action WindowHidden = null)
        {
            base.Hide();
            
        }

        public void SetTitle(string title)
        {
            _titleText.SetText(title);
        }
    }
}