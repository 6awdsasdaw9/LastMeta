using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Code.Services.Input;
using Code.Services.LanguageLocalization;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class MenuAdapter : IEventSubscriber
    {
        private readonly HudFacade _hudFacade;
        private readonly EventsFacade _eventsFacade;
        private readonly InputService _inputService;

        public MenuAdapter(EventsFacade eventsFacade, HudFacade hudFacade, InputService inputService)
        {
            _hudFacade = hudFacade;
            _eventsFacade = eventsFacade;
            _inputService = inputService;
            SubscribeToEvent(true);
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _hudFacade.Menu.Button.OnStartTap += CloseOpenWindow;
                _inputService.OnPressEsc += CloseOpenWindow;
                _hudFacade.Menu.CloseButton.OnStartTap += CloseOpenWindow;
                _hudFacade.Menu.Window.Settings.EngLanguage.OnStartTap += EngLanguageOnStartTap;
                _hudFacade.Menu.Window.Settings.RusLanguage.OnStartTap += RusLanguageOnStartTap;
            }
            else
            {
                _hudFacade.Menu.Button.OnStartTap -= CloseOpenWindow;
                _inputService.OnPressEsc -= CloseOpenWindow;
                _hudFacade.Menu.CloseButton.OnStartTap -= CloseOpenWindow;
                _hudFacade.Menu.Window.Settings.EngLanguage.OnStartTap -= EngLanguageOnStartTap;
                _hudFacade.Menu.Window.Settings.RusLanguage.OnStartTap -= RusLanguageOnStartTap;
            }
        }


        private void CloseOpenWindow()
        {
            var menuWindow = _hudFacade.Menu.Window;
            if (menuWindow.IsOpen)
            {
                menuWindow.HideWindow(() => _eventsFacade.HudEvents.WindowHiddenEvent(menuWindow));
            }
            else
            {
                menuWindow.ShowWindow(() => _eventsFacade.HudEvents.WindowShownEvent(menuWindow));
            }

            _eventsFacade.GameEvents.PauseEvent(menuWindow.IsOpen);//Стоит переложить эту задачу на адаптр окон
        }

        private void RusLanguageOnStartTap() => _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Rus);
        private void EngLanguageOnStartTap() => _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Eng);
    }
}