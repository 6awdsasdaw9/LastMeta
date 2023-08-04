using System.Collections.Generic;
using System.Linq;
using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Code.Services.EventsSubscribes;
using Code.UI.HeadUpDisplay.Windows;
using Zenject;

namespace Code.UI.HeadUpDisplay.Adapters
{
    public class WindowsAdapter: IEventsSubscriber 
    {
        private readonly EventsFacade _eventsFacade;
        
        private readonly List<IWindow> _openedWindows = new();
        private bool _menuWindowIsOpened;

        public  WindowsAdapter(DiContainer container)
        {
            _eventsFacade = container.Resolve<EventsFacade>();

            container.Resolve<EventSubsribersStorage>().Add(this);
            
            SubscribeToEvents(true);
        }
        
        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.HudEvents.OnWindowShown += AddOpenedWindowToList;
                _eventsFacade.HudEvents.OnWindowHidden += RemoveOpenedWindowFromList;
            }
            else
            {
                _eventsFacade.HudEvents.OnWindowShown -= AddOpenedWindowToList;
                _eventsFacade.HudEvents.OnWindowHidden -= RemoveOpenedWindowFromList;
            }
        }

        private void RemoveOpenedWindowFromList(IWindow window)
        {
            _openedWindows.Remove(window);
            if(_openedWindows.Any())return;
            _eventsFacade.HudEvents.CloseLastWindowEvent();
            _eventsFacade.GameEvents.PauseEvent(false);
        }

        private void AddOpenedWindowToList(IWindow window)
        {
            _openedWindows.Add(window);
            if(_openedWindows.Count != 1)return;
            _eventsFacade.HudEvents.OpenFirstWindowEvent();
            _eventsFacade.GameEvents.PauseEvent(true);
        }
    }
}