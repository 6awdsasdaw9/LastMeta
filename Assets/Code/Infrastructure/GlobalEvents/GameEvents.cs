using System;

namespace Code.Infrastructure.GlobalEvents
{
    public class GameEvents
    {
        public event Action OnStartPause;
        public void StartPause() => OnStartPause?.Invoke();

        public event Action OnStopPause;
        public void StopPause() => OnStopPause?.Invoke();
        
    }
}