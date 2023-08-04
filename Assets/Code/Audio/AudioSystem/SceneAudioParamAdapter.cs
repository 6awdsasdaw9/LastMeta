using Code.Character.Hero.HeroInterfaces;
using Code.Infrastructure.GlobalEvents;
using Code.Services.EventsSubscribes;
using Code.Services.SaveServices;
using Zenject;

namespace Code.Audio.AudioSystem
{
    public class SceneAudioParamAdapter: IEventsSubscriber, ISavedDataReader
    {
        private readonly SceneAudioController _audioController;
        private readonly EventsFacade _eventsFacade;
        private readonly IHero _hero;

        public SceneAudioParamAdapter(DiContainer container)
        {
            _audioController = container.Resolve<SceneAudioController>();
            _eventsFacade = container.Resolve<EventsFacade>();
            _hero = container.Resolve<IHero>();
            
            container.Resolve<EventSubsribersStorage>().Add(this);
            container.Resolve<SavedDataStorage>().Add(this);
            
            SubscribeToEvents(true);
        }


        public void SubscribeToEvents(bool flag)
        {
            _hero.Health.OnHealthChanged += OnChangeHeroHealthParam;
            _eventsFacade.GameEvents.OnPause += OnPause;
        }

        private void OnChangeHeroHealthParam()
        {
            var healthNormalize = _hero.Health.Current / _hero.Health.Max;
            _audioController.ChancgeHeroHealthParam(healthNormalize);
        }

        private void OnPause(bool isPause)
        {
            _audioController.ChangePauseParam(isPause);
        }
        
        public void LoadData(SavedData savedData)
        {
            
        }
    }
}