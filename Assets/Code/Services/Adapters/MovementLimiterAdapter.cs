using Code.Debugers;
using Code.Infrastructure.GlobalEvents;

namespace Code.Services.Adapters
{
    public class MovementLimiterAdapter : IEventSubscriber
    {
        private readonly MovementLimiter _movementLimiter;
        private readonly EventsFacade _eventsFacade;

        public MovementLimiterAdapter(MovementLimiter movementLimiter, EventsFacade eventsFacade)
        {
            _movementLimiter = movementLimiter;
            _eventsFacade = eventsFacade;
            SubscribeToEvent(true);
        }


        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _eventsFacade.HudEvents.OnCloseLastWindow += OnCloseLastWindow;
                _eventsFacade.HudEvents.OnOpenFirstWindow += OnOpenFirstWindow;
            }
        }

        private void OnOpenFirstWindow()
        {
            Logg.ColorLog("Disable Move");
            _movementLimiter.DisableMovement();
        }

        private void OnCloseLastWindow()
        {
            Logg.ColorLog("Enable Move");
            _movementLimiter.EnableMovement();
        }
    }
}