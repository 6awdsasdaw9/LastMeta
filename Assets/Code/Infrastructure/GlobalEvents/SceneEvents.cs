using System;

namespace Code.Infrastructure.GlobalEvents
{
    public class SceneEvents
    {
        public event Action OnLoadScene;
        public void LoadSceneEvent() => OnLoadScene?.Invoke();

        public event Action OnStartPause;
        public void StartPauseEvent() => OnStartPause?.Invoke();

        public event Action OnStopPause;
        public void StopPauseEvent() => OnStopPause?.Invoke();
        
    }
}