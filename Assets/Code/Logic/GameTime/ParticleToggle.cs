using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.DayOfTime.Interfaces;
using Code.Services;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class ParticleToggle : MonoBehaviour, IEventSubscriber, ITimeObserver
    {
        [SerializeField, EnumToggleButtons] private TimeOfDay _timeToEnable = TimeOfDay.Night;
        [SerializeField, EnumToggleButtons] private TimeOfDay _timeToDisable = TimeOfDay.Morning;
        [SerializeField] private ParticleSystem _particle;
        
        private GameClock _gameClock;
        private TimeEvents _timeEvents;
        private SceneEvents _sceneEvents;

        [Inject]
        private void Construct(GameClock gameClock, TimeEvents timeEvents, SceneEvents sceneEvents)
        {
            _gameClock = gameClock;
            _timeEvents = timeEvents;
            _sceneEvents = sceneEvents;
        }

        private void OnEnable()
        {
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _timeEvents.OnStartTimeOfDay += OnStartTimeOfDay;
                _sceneEvents.OnLoadScene += OnLoadScene;
            }
            else
            {
                _timeEvents.OnStartTimeOfDay -= OnStartTimeOfDay;
                _sceneEvents.OnLoadScene += OnLoadScene;
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
            _particle.Stop();
        }

        private void PlayParticle()
        {
            _particle.Play();
        }

    
    }
}