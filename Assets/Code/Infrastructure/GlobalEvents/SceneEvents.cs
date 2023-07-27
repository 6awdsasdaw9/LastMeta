using System;
using Code.Character.Hero.HeroInterfaces;

namespace Code.Infrastructure.GlobalEvents
{
    public class SceneEvents
    {
        public event Action OnExitScene;
        public void ExitSceneEvent() => OnExitScene?.Invoke();
        public event Action OnLoadScene;
        public void LoadSceneEvent() => OnLoadScene?.Invoke();

        public event Action OnStartPause;
        public void StartPauseEvent() => OnStartPause?.Invoke();

        public event Action OnStopPause;
        public void StopPauseEvent() => OnStopPause?.Invoke();
        
        public event Action<IHero> OnInitHero;
        public void InitHeroEvent(IHero hero) => OnInitHero?.Invoke(hero);
    }
}