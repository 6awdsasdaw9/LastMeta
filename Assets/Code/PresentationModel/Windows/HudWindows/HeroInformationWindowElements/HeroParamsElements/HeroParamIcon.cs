using Code.Data.Configs;
using Code.Logic.LanguageLocalization;
using TMPro;
using UnityEngine;

namespace Code.PresentationModel.Windows.HudWindows.HeroInformationWindowElements
{
    public class HeroParamIcon: HudElement, ILocalizationTitle, ILocalizationDescription
    {
        public HeroUpgradeParamType upgradeParamType;
        [SerializeField] private TextMeshProUGUI _textTitle;
        [SerializeField] private TextMeshProUGUI _textValue;
        
        public void SetTitle(string title)
        {
            _textTitle.SetText(title);
        }

        public void SetDescription(string description)
        {
            _textValue.SetText(description);
        }
    }
}