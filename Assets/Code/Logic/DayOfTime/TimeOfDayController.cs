using Code.Data.GameData;
using Code.Data.SavedDataPersistence;
using Code.Debugers;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{

    public class TimeOfDayController : ITickable, ISavedData
    {
        private float _dayTimeInSeconds;
        private float _currentSecondsOfDay;
        public float dayTimeNormalized => _currentSecondsOfDay / _dayTimeInSeconds;

        public TimeOfDay currentTimeOfDay { get; private set; }

        public delegate void TimeOfDayEvent();

        public event TimeOfDayEvent OnMorning;
        public event TimeOfDayEvent OnEvening;
        public event TimeOfDayEvent OnNight;


        private float _morningTime, _eveningTime, _nightTime;
        

        [Inject]
        private void Construct(SettingsData settingsData, SavedDataCollection savedDataCollection)
        {
            _dayTimeInSeconds = settingsData.dayTimeInSeconds;
           
            _morningTime = 0;
            _eveningTime = settingsData.durationOfDayTime;
            _nightTime =  settingsData.durationOfDayTime * 2;
            
            savedDataCollection.Add(this);
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
            if ( _currentSecondsOfDay < _eveningTime && currentTimeOfDay != TimeOfDay.Morning)
            {
                SetCurrentTimeOfDay(TimeOfDay.Morning);
            }
            else if (_currentSecondsOfDay > _eveningTime && _currentSecondsOfDay < _nightTime && currentTimeOfDay != TimeOfDay.Evening)
            {
                SetCurrentTimeOfDay(TimeOfDay.Evening);
            }
            else if (_currentSecondsOfDay > _nightTime && currentTimeOfDay != TimeOfDay.Night)
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

        public void LoadData(SavedData savedData)
        {
            _currentSecondsOfDay = savedData.currentTime;
            
            if (_currentSecondsOfDay < _eveningTime)
                SetCurrentTimeOfDay(TimeOfDay.Morning);
            else if (_currentSecondsOfDay > _eveningTime &&_currentSecondsOfDay < _nightTime)
                SetCurrentTimeOfDay(TimeOfDay.Evening);
            else
                SetCurrentTimeOfDay(TimeOfDay.Night);
        }

        public void SaveData(SavedData savedData)
        {
            savedData.currentTime = _currentSecondsOfDay;
        }
    }

    public enum TimeOfDay
    {
        Morning,
        Evening,
        Night
    }
}