using System.Collections.Generic;
using Code.Data.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class PointLightingToggle : MonoBehaviour
    {
        [SerializeField] private List<Light> _lights;

        private Tween _lightTween;
        private TimeOfDayController _timeOfDayController;
        private float _animationDuration;
        
        [Inject]
        private void Construct(TimeOfDayController timeOfDayController, GameSettings gameSettings)
        {
            _timeOfDayController = timeOfDayController;
            _animationDuration = gameSettings.DurationOfDayTime * 0.1f;
        }

        private void Start()
        {
            SubscribeToEvent();
        }

        private void OnDestroy()
        {
            UnsubscribeToEvent();
        }

        private void SubscribeToEvent()
        {
            _timeOfDayController.OnMorning += TurnOffEveningLights;
            _timeOfDayController.OnEvening += TurnOnEveningLights;
        }

        private void UnsubscribeToEvent()
        {
            _timeOfDayController.OnMorning -= TurnOffEveningLights;
            _timeOfDayController.OnEvening -= TurnOnEveningLights;
        }
        
        
        private void TurnOnEveningLights()
        {
            foreach (var light in _lights)
            {
                TurnOnLight(light);
            }
        }

        private void TurnOffEveningLights()
        {
            foreach (var light in _lights)
            {
                TurnOffLight(light);
            }
        }

        private void TurnOnLight(Light light)
        {
            var targetIntensity = 1;
            
            if(_lightTween is { active: true })
                _lightTween.Kill();
            
            _lightTween = light.DOIntensity(targetIntensity, _animationDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }

        private void TurnOffLight(Light light)
        {
            if(_lightTween is { active: true })
                _lightTween.Kill();
            
            _lightTween = light.DOIntensity(0, _animationDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDestroy);
        }
    }
}