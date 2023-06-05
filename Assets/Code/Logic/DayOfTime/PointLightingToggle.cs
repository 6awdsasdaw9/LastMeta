using System.Collections.Generic;
using Code.Data.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class PointLightingToggle : MonoBehaviour
    {
        [SerializeField] private TimeOfDay _timeOfEnable = TimeOfDay.Night;
        [SerializeField, Range(0.05f, 1)] private float _durationMultiplayer = 0.6f;
        [SerializeField] private List<Light> _lightPoints;

        private bool _isEmptyToggle => _lightPoints.Count == 0;
        private Tween _lightTween;
        private TimeOfDayController _timeOfDayController;
        private float _animationDuration;

        [Inject]
        private void Construct(TimeOfDayController timeOfDayController, GameSettings gameSettings)
        {
            _timeOfDayController = timeOfDayController;
            _animationDuration = gameSettings.DurationOfDayTime * _durationMultiplayer;
        }

        private void OnEnable()
        {
            if(_isEmptyToggle)
                return;
            
            SetLighting();
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            if(_isEmptyToggle)
                return;
            
            _lightTween?.Kill();
            SubscribeToEvent(false);
        }

        private void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _timeOfDayController.OnMorning += DisableLightPoints;
                switch (_timeOfEnable)
                {
                    case TimeOfDay.Evening:
                        _timeOfDayController.OnEvening += EnableLightPoints;
                        break;
                    case TimeOfDay.Night:
                        _timeOfDayController.OnNight += EnableLightPoints;
                        break;
                    case TimeOfDay.Morning:
                    default:
                        break;
                }
            }
            else
            {
                _timeOfDayController.OnMorning -= DisableLightPoints;
                switch (_timeOfEnable)
                {
                    case TimeOfDay.Evening:
                        _timeOfDayController.OnEvening -= EnableLightPoints;
                        break;
                    case TimeOfDay.Night:
                        _timeOfDayController.OnNight -= EnableLightPoints;
                        break;
                    case TimeOfDay.Morning:
                    default:
                        break;
                }
            }
        }

        private void SetLighting()
        {
            var intensity = 0;
            if (_timeOfDayController.CurrentTimeOfDay == _timeOfEnable)
            {
                intensity = 1;
            }

            foreach (var lightPoint in _lightPoints)
            {
                lightPoint.intensity = intensity;
            }
        }

        private void EnableLightPoints()
        {
            foreach (var lightPoint in _lightPoints)
            {
                EnableLight(lightPoint);
            }
        }

        private void DisableLightPoints()
        {
            foreach (var lightPoint in _lightPoints)
            {
                DisableLight(lightPoint);
            }
        }
        
        private void EnableLight(Light lightPoint)
        {
            var targetIntensity = 1;

            _lightTween?.Kill();
            _lightTween = lightPoint.DOIntensity(targetIntensity, _animationDuration);
        }

        private void DisableLight(Light lightPoint)
        {
            _lightTween?.Kill();
            _lightTween = lightPoint.DOIntensity(0, _animationDuration);
        }
    }
}