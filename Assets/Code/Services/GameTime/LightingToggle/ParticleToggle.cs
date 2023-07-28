using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.TimingObjects.TimeObserverses.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Services.GameTime.LightingToggle
{
    public class ParticleToggle : MonoBehaviour, IEventsSubscriber, ITimeObserver
    {
        [SerializeField, EnumToggleButtons] private TimeOfDay _timeToEnable = TimeOfDay.Night;
        [SerializeField, EnumToggleButtons] private TimeOfDay _timeToDisable = TimeOfDay.Morning;
        [SerializeField] private ParticleSystem _particle;
        
        private GameClock _gameClock;
        private EventsFacade _eventsFacade;

        [Inject]
        private void Construct(DiContainer container)
        {
            _gameClock = container.Resolve<GameClock>();
            _eventsFacade = container.Resolve<EventsFacade>();
            
            container.Resolve<EventSubsribersStorage>().Add(this);
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.SceneEvents.OnLoadScene += OnLoadScene;
                _eventsFacade.TimeEvents.OnStartTimeOfDay += OnStartTimeOfDay;
            }
            else
            {
                _eventsFacade.SceneEvents.OnLoadScene -= OnLoadScene;
                _eventsFacade.TimeEvents.OnStartTimeOfDay -= OnStartTimeOfDay;
            }
        }
        public void OnLoadScene()
        {
            if (_gameClock.CurrentTime.TimeOfDay == _timeToEnable.ToString())
            {
                PlayParticle();
            }
            else
            {
                StopParticle();
            }
        }

        public void OnStartTimeOfDay(TimeOfDay timeOfDay)
        {
            if (_gameClock.CurrentTime.TimeOfDay == _timeToEnable.ToString())
            {
                PlayParticle();
            }
            else if(_gameClock.CurrentTime.TimeOfDay ==  _timeToDisable.ToString())
            {
                StopParticle();
            }
        }
        private void StopParticle()
        {
            _particle?.Stop();
        }

        private void PlayParticle()
        {
            _particle?.Play();
        }

    
    }
}