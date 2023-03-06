using Code.Data;
using Code.Data.Configs;
using Code.Data.DataPersistence;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{

    public class TimeOfDayController : MonoBehaviour, ITickable, IDataPersistence
    {
        private float _dayTimeInSeconds;
        private float _currentSecondsOfDay;
        public float dayTimeNormalized => _currentSecondsOfDay / _dayTimeInSeconds;
        public float durationOfDay => _dayTimeInSeconds / 3;
        public TimeOfDay currentTimeOfDay { get; private set; }

        public delegate void TimeOfDayEvent();

        public event TimeOfDayEvent OnMorning;
        public event TimeOfDayEvent OnEvening;
        public event TimeOfDayEvent OnNight;


        [Inject]
        private void Construct(GameSettings settings)
        {
            _dayTimeInSeconds = settings.dayTimeInSeconds;
        }

        public void Tick()
        {
            ClockMovement();
            CheckTimeOfDay();
        }

        private void ClockMovement()
        {
            _currentSecondsOfDay += Time.deltaTime;
            if (_currentSecondsOfDay >= _dayTimeInSeconds)
                _currentSecondsOfDay = 0;
        }

        private void CheckTimeOfDay()
        {
            if (_currentSecondsOfDay < durationOfDay && currentTimeOfDay != TimeOfDay.Morning)
            {
                SetCurrentTimeOfDay(TimeOfDay.Morning);
            }
            else if (_currentSecondsOfDay < durationOfDay * 2 && currentTimeOfDay != TimeOfDay.Evening)
            {
                SetCurrentTimeOfDay(TimeOfDay.Evening);
            }
            else if (currentTimeOfDay != TimeOfDay.Night)
            {
                SetCurrentTimeOfDay(TimeOfDay.Night);
            }
        }

        private void SetCurrentTimeOfDay(TimeOfDay newTimeOfDay)
        {
            if (newTimeOfDay == currentTimeOfDay) return;

            currentTimeOfDay = newTimeOfDay;

            switch (currentTimeOfDay)
            {
                case TimeOfDay.Morning:
                    OnMorning?.Invoke();
                    break;
                case TimeOfDay.Evening:
                    OnEvening?.Invoke();
                    break;
                case TimeOfDay.Night:
                    OnNight?.Invoke();
                    break;
            }
        }

        public void LoadData(ProgressData progressData)
        {
            Log.ColorLog("LoadTime");
            _currentSecondsOfDay = progressData.currentTime;
            
            if (_currentSecondsOfDay < durationOfDay)
                SetCurrentTimeOfDay(TimeOfDay.Morning);
            else if (_currentSecondsOfDay < durationOfDay * 2)
                SetCurrentTimeOfDay(TimeOfDay.Evening);
            else
                SetCurrentTimeOfDay(TimeOfDay.Night);
        }

        public void SaveData(ProgressData progressData)
        {
            progressData.currentTime = _currentSecondsOfDay;
        }
    }

    public enum TimeOfDay
    {
        Morning,
        Evening,
        Night
    }
}