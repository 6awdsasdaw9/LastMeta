using System;
using Code.Logic.DayOfTime;
using Code.Services;
using Code.UI.HeadUpDisplay;
using UnityEngine;
using Zenject;

namespace Code.UI.Adaptors
{
    public class TimeOfDayAdapter: MonoBehaviour
    {
        private SliderController _sliderTimeOfDay;
        private TimeOfDayController _timeOfDayController;

        private bool _isEmptyAdapter => _sliderTimeOfDay == null || _timeOfDayController == null;
        
        [Inject]
        private void Construct(Hud hud, TimeOfDayController timeOfDayController)
        {
            _sliderTimeOfDay = hud.SliderTimeOfDay;
            _timeOfDayController = timeOfDayController;

            if (_isEmptyAdapter)
            {
                enabled = false;
            }
        }

        private void LateUpdate()
        {
            _sliderTimeOfDay.SetValue(_timeOfDayController.DayTimeNormalized);
        }

    }
}