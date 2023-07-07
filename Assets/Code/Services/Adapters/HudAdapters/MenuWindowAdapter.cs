using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.LanguageLocalization;
using Code.PresentationModel.HeadUpDisplay;
using Code.Services.Input;

namespace Code.Logic.Adaptors
{
    public class MenuWindowAdapter
    {
        private readonly EventsFacade _eventsFacade;
        private readonly Hud _hud;
        private readonly InputService _inputService;
        
        public MenuWindowAdapter(EventsFacade eventsFacade, Hud hud, InputService inputService)
        {
            _eventsFacade = eventsFacade;
            _hud = hud;
            _inputService = inputService;
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _hud.Menu.Button.OnStartTap += CloseOpenWindow;
                _inputService.OnPressEsc += CloseOpenWindow;
                _hud.Menu.CloseButton.OnStartTap += CloseOpenWindow;
                _hud.Menu.Window.EngLanguage.OnStartTap += EngLanguageOnStartTap;
                _hud.Menu.Window.RusLanguage.OnStartTap += RusLanguageOnStartTap;
            }
            else
            {
                _hud.Menu.Button.OnStartTap -= CloseOpenWindow;
                _inputService.OnPressEsc -= CloseOpenWindow;
                _hud.Menu.CloseButton.OnStartTap -= CloseOpenWindow;
                _hud.Menu.Window.EngLanguage.OnStartTap -= EngLanguageOnStartTap;
                _hud.Menu.Window.RusLanguage.OnStartTap -= RusLanguageOnStartTap;
            }
        }


        private void CloseOpenWindow()
        {
            if (_hud.Menu.Window.IsOpen) _hud.Menu.Window.HideWindow(() => _hud.OnUIWindowHidden?.Invoke());
            else _hud.Menu.Window.ShowWindow(() => _hud.OnUIWindowShown?.Invoke());
        }

        private void RusLanguageOnStartTap() => _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Rus);
        private void EngLanguageOnStartTap() => _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Eng);
    }
}