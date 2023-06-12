using System;
using System.Linq;
using Code.Data.Configs;
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
    public class DirectionLightingToggle : MonoBehaviour, IEventSubscriber, ITimeObserver
    {
        [SerializeField] private Light _directionLight;

        private GameClock _gameClock;
        private TimeEvents _timeEvents;
        private SceneEvents _sceneEvents;
        private GameSceneData _gameSceneData;
        
        private float _animationDuration;
        private Sequence _sequence;
        
        [Inject]
        private void Construct(GameSceneData gameSceneData, GameClock gameClock, TimeEvents timeEvents,SceneEvents sceneEvents)
        {
            _gameClock = gameClock;
            _gameSceneData = gameSceneData;
            _animationDuration = gameClock.DurationOfDayTime;
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
                _sceneEvents.OnLoadScene += OnLoadScene; 
                _timeEvents.OnStartTimeOfDay += OnStartTimeOfDay;
            }
            else
            {
                _sceneEvents.OnLoadScene -= OnLoadScene; 
                _timeEvents.OnStartTimeOfDay -= OnStartTimeOfDay;
            }
        }

        public void OnLoadScene()
        {
            if (Enum.TryParse(_gameClock.CurrentTime.TimeOfDay, out TimeOfDay timeOfDay))
            {
                SetLighting(timeOfDay);
            }
        }

        public void OnStartTimeOfDay(TimeOfDay timeOfDay)
        {
            var param = _gameSceneData.SceneParams.TimeOfDaySettings.GetLightParams(timeOfDay);
            if(param == null)
                return;
            PlayAnimationLighting(param.DirectionLightAngle, param.DirectionLightIntensity,_animationDuration);
        }
        
        private void PlayAnimationLighting(Vector3 angle, float intensity, float duration = 0)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(_directionLight.transform.DOLocalRotate(angle, duration));
            _sequence.Append(_directionLight.DOIntensity(intensity, duration));
        }
        
        private void SetLighting(TimeOfDay timeOfDay)
        {
            var param = _gameSceneData.SceneParams.TimeOfDaySettings.GetLightParams(timeOfDay);
            if(param == null)
                return;
            _directionLight.intensity = param.DirectionLightIntensity;
            _directionLight.transform.rotation = Quaternion.Euler(param.DirectionLightAngle);
        }
        
        
#if UNITY_EDITOR

        [Title("Edit Mode")]
        [SerializeField] private ScenesConfig _testSceneConfig;
        [SerializeField, EnumToggleButtons] private TimeOfDay timeOfDay;
        [SerializeField] private Constants.Scenes _scene;
        
        [Button]
        private void ImitateLight()
        {
            var param = _testSceneConfig.ScenesParams.FirstOrDefault(p => p.Scene == _scene)?.TimeOfDaySettings
                .GetLightParams(timeOfDay);
            if(param == null)
                return;
            _directionLight.intensity = param.DirectionLightIntensity;
            _directionLight.transform.rotation = Quaternion.Euler(param.DirectionLightAngle);
        }
#endif
    }
}