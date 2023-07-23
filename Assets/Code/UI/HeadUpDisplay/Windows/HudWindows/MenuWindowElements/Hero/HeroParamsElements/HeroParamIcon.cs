using Code.Data.Configs.HeroConfigs;
using Code.Services.LanguageLocalization;
using Code.UI.HeadUpDisplay.HudElements;
using TMPro;
using UnityEngine;

namespace Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero.HeroParamsElements
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