using System.Linq;
using Code.Data.Configs.TextsConfigs;
using Code.Infrastructure.GlobalEvents;
using Code.UI.HeadUpDisplay;

namespace Code.Services.LanguageLocalization
{
    public class HudLocalization
    {
        private readonly EventsFacade _eventsFacade;
        private readonly HudFacade _hudFacade;
        private readonly TextConfigRus _textConfigRus;
        private readonly TextConfigEng _textConfigEng;

        public HudLocalization(EventsFacade eventsFacade, HudFacade hudFacade, TextConfigRus textConfigRus,
            TextConfigEng textConfigEng)
        {
            _eventsFacade = eventsFacade;
            _hudFacade = hudFacade;
            _textConfigRus = textConfigRus;
            _textConfigEng = textConfigEng;
            _eventsFacade.GameEvents.OnChoiceLanguage += OnChoiceLanguage;
        }

        private void OnChoiceLanguage(Language language)
        {
            if (language == Language.Rus)
            {
                SetLanguage(_textConfigRus);
            }
            else
            {
                SetLanguage(_textConfigEng);
            }
        }

        private void SetLanguage(TextConfig textConfig)
        {
            _hudFacade.Menu?.Window.Settings.SetTitle(textConfig.HudNamings.NameMenuWindow);
            
            if(_hudFacade.GameMode == Constants.GameMode.Real)
                return;

            RefreshHeroPanel(textConfig);
        }

        private void RefreshHeroPanel(TextConfig textConfig)
        {
            var heroWindow = _hudFacade.Menu.Window.Hero;
            heroWindow.SetTitle(textConfig.HudNamings.NameHeroWindow);
            foreach (var icon in heroWindow.ArtifactsPanel.ArtifactIcons)
            {
                var data = textConfig.HudNamings.ArtifactsDescriptionText.FirstOrDefault(a =>
                    a.Type == icon.Type);

                if (data == null || icon.DescriptionPanel == null)
                    continue;
                icon.DescriptionPanel.SetTitle(data.Title);
                icon.DescriptionPanel.SetDescription(data.Description);
            }

            foreach (var icon in heroWindow.HeroParamPanel.ParamIcons)
            {
                var data = textConfig.HudNamings.HeroParamsText.FirstOrDefault(a =>
                    a.upgradeParamType == icon.upgradeParamType);

                if (data == null)
                    continue;
                icon.SetTitle(data.Title);
            }
        }
    }
}