using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.LanguageLocalization;
using Code.PresentationModel.HeadUpDisplay;

namespace Code.Logic.Adaptors
{
    public class MenuWindowAdapter
    {
        private readonly EventsFacade _eventsFacade;
        private readonly Hud _hud;


        public MenuWindowAdapter(EventsFacade eventsFacade, Hud hud)
        {
            _eventsFacade = eventsFacade;
            _hud = hud;
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _hud.Menu.Button.OnStartTap += MenuButtonOnStartTap;
                _hud.Menu.CloseButton.OnStartTap += MenuButtonOnStartTap;
                _hud.Menu.Window.EngLanguage.OnStartTap += EngLanguageOnStartTap;
                _hud.Menu.Window.RusLanguage.OnStartTap += RusLanguageOnStartTap;
            }
            else
            {
                _hud.Menu.Button.OnStartTap -= MenuButtonOnStartTap;
                _hud.Menu.CloseButton.OnStartTap -= MenuButtonOnStartTap;
                _hud.Menu.Window.EngLanguage.OnStartTap -= EngLanguageOnStartTap;
                _hud.Menu.Window.RusLanguage.OnStartTap -= RusLanguageOnStartTap;
            }
        }


        private void RusLanguageOnStartTap()
        {
            _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Rus);
        }

        private void EngLanguageOnStartTap()
        {
            _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Eng);
        }

        private void MenuButtonOnStartTap()
        {
            if (_hud.Menu.Window.IsOpen)
            {
                _hud.Menu.Window.HideWindow(() => _hud.OnUIWindowHidden?.Invoke());
            }
            else
            {
                Logg.ColorLog("Show");
                _hud.Menu.Window.ShowWindow(() => _hud.OnUIWindowShown?.Invoke());
            }
        }
    }
}