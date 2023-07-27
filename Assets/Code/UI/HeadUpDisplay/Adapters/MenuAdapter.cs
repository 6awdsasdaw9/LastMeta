using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Code.Services.Input;
using Code.Services.LanguageLocalization;
using Code.UI.HeadUpDisplay.Windows;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Hero;
using Code.UI.HeadUpDisplay.Windows.HudWindows.MenuWindowElements.Settings;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class MenuAdapter : IEventsSubscriber
    {
        private readonly HudFacade _hudFacade;
        private readonly EventsFacade _eventsFacade;
        private readonly InputService _inputService;

        private IWindow _currentWindow;

        public MenuAdapter(EventsFacade eventsFacade, HudFacade hudFacade, InputService inputService)
        {
            _hudFacade = hudFacade;
            _eventsFacade = eventsFacade;
            _inputService = inputService;
            
            SubscribeToEvents(true);
            InitCurrentWindow();
        }

        private void InitCurrentWindow()
        {
            _currentWindow =_hudFacade.Menu.Window.Hero; 
            _currentWindow.ShowWindow();
            _hudFacade.Menu.Window.Settings.HideWindow();
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _hudFacade.Menu.Button.OnStartTap += CloseOpenWindow;
                _inputService.OnPressEsc += CloseOpenWindow;
                _hudFacade.Menu.CloseButton.OnStartTap += CloseOpenWindow;

                //new
                _hudFacade.Menu.Window.Hero.FolderButton.OnStartTap += HeroFolderButtonOnStartTap;
                _hudFacade.Menu.Window.Settings.FolderButton.OnStartTap += SettingsFolderButtonOnStartTap;

                //todo эта часть премещается в адаптер settings panel
                _hudFacade.Menu.Window.Settings.EngLanguage.OnStartTap += EngLanguageOnStartTap;
                _hudFacade.Menu.Window.Settings.RusLanguage.OnStartTap += RusLanguageOnStartTap;
            }
            else
            {
                _hudFacade.Menu.Button.OnStartTap -= CloseOpenWindow;
                _inputService.OnPressEsc -= CloseOpenWindow;
                _hudFacade.Menu.CloseButton.OnStartTap -= CloseOpenWindow;
                
                //new
                _hudFacade.Menu.Window.Hero.FolderButton.OnStartTap -= HeroFolderButtonOnStartTap;
                _hudFacade.Menu.Window.Settings.FolderButton.OnStartTap -= SettingsFolderButtonOnStartTap;

                //todo эта часть премещается в адаптер settings panel
                _hudFacade.Menu.Window.Settings.EngLanguage.OnStartTap -= EngLanguageOnStartTap;
                _hudFacade.Menu.Window.Settings.RusLanguage.OnStartTap -= RusLanguageOnStartTap;
            }
        }

        private void SettingsFolderButtonOnStartTap()
        {
            if (_currentWindow is SettingsPanel) return;
            ShowWindow(_hudFacade.Menu.Window.Settings);
        }

        private void HeroFolderButtonOnStartTap()
        {
            if (_currentWindow is HeroPanel) return;
            ShowWindow(_hudFacade.Menu.Window.Hero);
        }


        private void ShowWindow<T>(T window) where T : IWindow
        {
            _currentWindow?.HideWindow();
            _currentWindow = window;
            _currentWindow.ShowWindow();
        }

        private void CloseOpenWindow()
        {
            var menuWindow = _hudFacade.Menu.Window;
            if (menuWindow == null) return;
            if (menuWindow.IsOpen)
            {
                menuWindow.HideWindow(() => _eventsFacade.HudEvents.WindowHiddenEvent(menuWindow));
            }
            else
            {
                menuWindow.ShowWindow(() => _eventsFacade.HudEvents.WindowShownEvent(menuWindow));
            }
        }

        private void RusLanguageOnStartTap() => _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Rus);
        private void EngLanguageOnStartTap() => _eventsFacade.HudEvents.PressButtonLanguageEvent(Language.Eng);
    }
}