using System;
using Code.Data.Configs;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class SkyParticleController : MonoBehaviour, IEventSubscriber
    {
        [SerializeField] private ParticleSystem _starsParticle;
        private TimeOfDayController _timeOfDayController;
        
        [Inject]
        private void Construct(TimeOfDayController timeOfDayController)
        {
            _timeOfDayController = timeOfDayController;
        }


        private void OnEnable()
        {
            SubscribeToEvent(true);
            if (_timeOfDayController.CurrentTimeOfDay != TimeOfDay.Night)
            {
                StopStarParticle();
            }
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _timeOfDayController.OnNight += PlayStarParticle;
                _timeOfDayController.OnMorning += StopStarParticle;
            }
            else
            {
                _timeOfDayController.OnNight -= PlayStarParticle;
                _timeOfDayController.OnMorning -= StopStarParticle;
            }
        }

        private void StopStarParticle()
        {
            _starsParticle.Stop();
        }

        private void PlayStarParticle()
        {
            _starsParticle.Play();
        }
    }
}