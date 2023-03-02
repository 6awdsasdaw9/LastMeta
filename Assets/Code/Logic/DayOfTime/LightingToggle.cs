using Code.Data;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class LightingToggle : MonoBehaviour
    {
        [SerializeField] private Light _directionLight;

        private float _morningIntensity, _eveningIntensity, _nightIntensity;
        private Vector3 _morningAngle, _eveningAngle, _nightAngle;

        private TimeOfDayController _timeOfDayController;

        private float delay;
        [Inject]
        private void Construct(TimeOfDayController timeOfDayController, GameSettings gameSettings)
        {
            delay = gameSettings.dayTimeInSeconds / 3;
            _timeOfDayController = timeOfDayController;
            
            _morningAngle = gameSettings.morningAngle;
            _eveningAngle = gameSettings.eveningAngle;
            _nightAngle = gameSettings.nightAngle;

            _morningIntensity = gameSettings.morningIntensity;
            _eveningIntensity = gameSettings.eveningIntensity;
            _nightIntensity = gameSettings.nightIntensity;
        }

        private void Start()
        {
            SubscribeToEvent();
            SetLighting();
        }

        private void OnDisable()
        {
            UnsubscribeToEvent();
        }

        private void SubscribeToEvent()
        {
            _timeOfDayController.OnMorning += SetMorningLighting;
            _timeOfDayController.OnEvening += SetEveningLighting;
            _timeOfDayController.OnNight += SetNightLighting;
        }
        
        private void UnsubscribeToEvent()
        {
            _timeOfDayController.OnMorning -= SetMorningLighting;
            _timeOfDayController.OnEvening -= SetEveningLighting;
            _timeOfDayController.OnNight -= SetNightLighting;
        }

        private void SetLighting()
        {
            switch (_timeOfDayController.CurrentTimeOfDay)
            {
                case TimeOfDay.Morning:
                    SetMorningLighting();
                    break;
                case TimeOfDay.Evening:
                    SetEveningLighting();
                    break;
                case TimeOfDay.Night:
                    SetNightLighting();
                    break;
            }
        }

        private void SetMorningLighting()
        {
            _directionLight.transform.DORotate(_morningAngle,delay);
            _directionLight.DOIntensity(_morningIntensity, delay);
        }

        private void SetEveningLighting()
        {
            _directionLight.transform.DORotate(_eveningAngle, delay);
            _directionLight.DOIntensity(_eveningIntensity, delay);
        }

        private void SetNightLighting()
        {
            _directionLight.transform.DORotate(_nightAngle, delay);
            _directionLight.DOIntensity(_nightIntensity, delay);
        }
    }
}