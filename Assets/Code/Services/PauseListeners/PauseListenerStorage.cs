using System.Collections.Generic;
using Code.Infrastructure.GlobalEvents;
using Code.Services.EventsSubscribes;
using Zenject;

namespace Code.Services.PauseListeners
{
    public class PauseListenerStorage : IEventsSubscriber
    {
        private readonly EventsFacade _eventsFacade;
        private readonly List<IPauseListener> _pauseListeners = new();
        private readonly List<IResumeListener> _resumeListeners = new();

        public PauseListenerStorage(DiContainer container)
        {
            _eventsFacade = container.Resolve<EventsFacade>();
            container.Resolve<EventSubsribersStorage>().Add(this);
            SubscribeToEvents(true);
        }
        
        public void Add(IPauseListener pauseListener) => _pauseListeners.Add(pauseListener);
        public void Add(IResumeListener resumeListener)
        {
            _pauseListeners.Add(resumeListener);
            _resumeListeners.Add(resumeListener);
        }

        private void CleanUp()
        {
            _pauseListeners.Clear();
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.GameEvents.OnPause += OnPause;
            }
            else
            {
                _eventsFacade.GameEvents.OnPause -= OnPause;
            }
        }

        private void OnPause(bool isPause)
        {
            if (isPause)
            {
                foreach (var listener in _pauseListeners)
                {
                    listener.OnPause();
                }
          
            }
            else
            {
                foreach (var listener in _resumeListeners)
                {
                    listener.OnResume();
                }
            }
        }
    }
}