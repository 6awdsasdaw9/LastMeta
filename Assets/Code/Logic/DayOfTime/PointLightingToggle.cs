using System.Collections.Generic;
using Code.Data.Configs;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class PointLightingToggle : MonoBehaviour
    {
        [SerializeField] private List<Light> _lightPoints;
        ///[SerializeField] private float _durationMultiplayer
        
        private Tween _lightTween;
        private TimeOfDayController _timeOfDayController;
        private float _animationDuration;

        [Inject]
        private void Construct(TimeOfDayController timeOfDayController, GameSettings gameSettings)
        {
            _timeOfDayController = timeOfDayController;
            _animationDuration = gameSettings.DurationOfDayTime * 0.15f;
        }

        private void Start()
        {
            SetLighting();
            SubscribeToEvent();
        }

        private void SetLighting()
        {
            if (_timeOfDayController.CurrentTimeOfDay == TimeOfDay.Night)
            {
                foreach (var nightLight in _lightPoints)
                {
                    nightLight.intensity = 1;
                }
            }
            else
            {
                foreach (var nightLight in _lightPoints)
                {
                    nightLight.intensity = 0;
                }
            }
        }

        private void OnDestroy()
        {
            UnsubscribeToEvent();
        }

        private void SubscribeToEvent()
        {
            _timeOfDayController.OnMorning += DisableEveningLights;
            _timeOfDayController.OnNight += EnableEveningLights;
        }

        private void UnsubscribeToEvent()
        {
            _timeOfDayController.OnMorning -= DisableEveningLights;
            _timeOfDayController.OnNight -= EnableEveningLights;
        }


        private void EnableEveningLights()
        {
            foreach (var light in _lightPoints)
            {
                EnableLight(light);
            }
        }

        private void DisableEveningLights()
        {
            foreach (var light in _lightPoints)
            {
                DisableLight(light);
            }
        }


        private void EnableLight(Light light)
        {
            var targetIntensity = 1;


            /*_lightTween =*/
            light.DOIntensity(targetIntensity, _animationDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }

        private void DisableLight(Light light)
        {
            /*_lightTween =*/
            light.DOIntensity(0, _animationDuration)
                .SetLink(gameObject, LinkBehaviour.KillOnDisable);
        }
    }
}