using System.Collections.Generic;
using Code.Infrastructure.GlobalEvents;

namespace Code.Services
{
    public class EventSubsribersStorage
    {
        private readonly EventsFacade _eventsFacade;
        private readonly List<IEventSubscriber> _eventSubscribers = new();

        public EventSubsribersStorage(EventsFacade eventsFacade)
        {
            _eventsFacade = eventsFacade;
            _eventsFacade.SceneEvents.OnExitScene += OnExitScene;
        }
        
        public void Add(IEventSubscriber savedData) => _eventSubscribers.Add(savedData);
        
        private void OnExitScene()
        {
            foreach (var subscriber in _eventSubscribers)
            {
                subscriber.SubscribeToEvent(false);
            }
            CleanUp();
        }
        
        private void CleanUp() => _eventSubscribers.Clear();
    }
}