using Code.Infrastructure.GlobalEvents;
using Zenject;

namespace Code.Services.Adapters
{
    public class MovementLimiterAdapter : IEventsSubscriber
    {
        private readonly MovementLimiter _movementLimiter;
        private readonly EventsFacade _eventsFacade;

        public MovementLimiterAdapter(DiContainer container)
        {
            _movementLimiter = container.Resolve<MovementLimiter>();
            _eventsFacade = container.Resolve<EventsFacade>();
            
            container.Resolve<EventSubsribersStorage>().Add(this);
            
            SubscribeToEvents(true);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.HudEvents.OnCloseLastWindow += OnCloseLastWindow;
                _eventsFacade.HudEvents.OnOpenFirstWindow += OnOpenFirstWindow;
            }
            else
            {
                _eventsFacade.HudEvents.OnCloseLastWindow -= OnCloseLastWindow;
                _eventsFacade.HudEvents.OnOpenFirstWindow -= OnOpenFirstWindow;
            }
        }

        private void OnOpenFirstWindow()
        {
            _movementLimiter.DisableMovement();
        }

        private void OnCloseLastWindow()
        {
            _movementLimiter.EnableMovement();
        }
    }
}