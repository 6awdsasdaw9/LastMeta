using System.Linq;
using Code.Data.Configs;
using Code.Infrastructure.GlobalEvents;
using Code.PresentationModel.HeadUpDisplay;

namespace Code.Logic.LanguageLocalization
{
    public class HudLocalization
    {
        private readonly EventsFacade _eventsFacade;
        private readonly Hud _hud;
        private readonly TextConfigRus _textConfigRus;
        private readonly TextConfigEng _textConfigEng;

        public HudLocalization(EventsFacade eventsFacade, Hud hud, TextConfigRus textConfigRus,
            TextConfigEng textConfigEng)
        {
            _eventsFacade = eventsFacade;
            _hud = hud;
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
            _hud.Menu?.Window.SetTitle(textConfig.HudNamings.NameMenuWindow);
            
            if(_hud.GameMode == Constants.GameMode.Real)
                return;
            
            _hud.HeroInformation?.Window.SetTitle(textConfig.HudNamings.NameHeroInformationWindow);
            foreach (var icon in _hud.HeroInformation.Window.ArtifactsPanel.ArtifactIcons)
            {
                var data = textConfig.HudNamings.ArtifactsDescriptionText.FirstOrDefault(a =>
                    a.Type == icon.Type);

                if (data == null)
                    continue;
                icon.DescriptionPanel.SetTitle(data.Title);
                icon.DescriptionPanel.SetDescription(data.Description);
            }
            
            foreach (var icon in _hud.HeroInformation.Window.HeroParamPanel.ParamIcons)
            {
                var data = textConfig.HudNamings.HeroParamsText.FirstOrDefault(a =>
                    a.ParamType == icon.ParamType);

                if (data == null)
                    continue;
                icon.SetTitle(data.Title);
            }
        }
    }
}