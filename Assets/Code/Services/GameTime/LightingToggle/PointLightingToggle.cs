using System.Collections.Generic;
using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.DayOfTime.Interfaces;
using Code.Services;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;


namespace Code.Logic.DayOfTime
{
    public class PointLightingToggle : MonoBehaviour, IEventSubscriber, ITimeObserver
    {
        [SerializeField, EnumToggleButtons] private TimeOfDay _timeToEnable = TimeOfDay.Night;
        [SerializeField, EnumToggleButtons] private TimeOfDay _timeToDisable = TimeOfDay.Morning;

        [SerializeField, MinMaxRange(0.05f, 1)]
        private RangedFloat _durationMultiplayer;

        [SerializeField, MinMaxRange(0.05f, 1.5f)]
        private RangedFloat _maxIntensity;

        [SerializeField, MinMaxRange(0.05f, 1.5f)]
        private RangedFloat _minIntensity;

        [SerializeField] private List<Light> _lightPoints;

        private GameClock _gameClock;
        private EventsFacade _eventsFacade;
        private bool _isEmptyToggle => _lightPoints.Count == 0;
        private Tween _lightTween;
        private float _animationDuration => _gameClock.DayTimeInSeconds * 0.3f * _durationMultiplayer.GetRandom();

        [Inject]
        private void Construct(GameClock gameClock, EventsFacade eventsFacade)
        {
            _gameClock = gameClock;
            _eventsFacade = eventsFacade;
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
            if (_isEmptyToggle)
                return;
            
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
                EnableLightPoints();
            }
            else
            {
                DisableLightPoints();
            }
        }

        public void OnStartTimeOfDay(TimeOfDay timeOfDay)
        {
            if (timeOfDay == _timeToEnable)
            {
                EnableLightPoints();
            }
            else if (timeOfDay == _timeToDisable)
            {
                DisableLightPoints();
            }
        }
        
        private void EnableLightPoints()
        {
            foreach (var lightPoint in _lightPoints)
            {
                SetLighting(lightPoint, _maxIntensity.GetRandom(), _animationDuration);
            }
        }
        
        private void DisableLightPoints()
        {
            foreach (var lightPoint in _lightPoints)
            {
                SetLighting(lightPoint, _minIntensity.GetRandom());
            }
        }

        private void SetLighting(Light lightPoint, float intensity, float duration = 0, float delay = 0, Ease ease = Ease.Linear)
        {
            lightPoint.DOIntensity(intensity, duration)
                .SetDelay(delay)
                .SetEase(ease)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

       
    }
}