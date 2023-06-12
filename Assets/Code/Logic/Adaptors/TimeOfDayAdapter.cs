using Code.Logic.DayOfTime;
using Code.PresentationModel;
using Code.PresentationModel.HeadUpDisplay;
using UnityEngine;
using Zenject;

namespace Code.Logic.Adaptors
{
    public class TimeOfDayAdapter: MonoBehaviour
    {
        private SliderController _sliderTimeOfDay;
        private GameClock _gameClock;

        private bool _isEmptyAdapter => _sliderTimeOfDay == null || _gameClock == null;
        
        [Inject]
        private void Construct(Hud hud, GameClock gameClock)
        {
            _sliderTimeOfDay = hud.SliderTimeOfDay;
            _gameClock = gameClock;

            if (_isEmptyAdapter)
            {
                enabled = false;
            }
        }

        private void LateUpdate()
        {
            _sliderTimeOfDay.SetValue(_gameClock.DayTimeNormalized);
        }

    }
}