using System;
using Code.Data.Configs;
using Code.Services.SaveServices;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{

    public class TimeOfDayController : ITickable, ISavedData
    {
        private float _dayTimeInSeconds;
        private float _currentSecondsOfDay;
        public float DayTimeNormalized => _currentSecondsOfDay / _dayTimeInSeconds;

        public TimeOfDay CurrentTimeOfDay { get;private set; }


        public delegate void TimeOfDayEvent();
        public event TimeOfDayEvent OnMorning;
        public event TimeOfDayEvent OnEvening;
        public event TimeOfDayEvent OnNight;

        private float  _eveningTime, _nightTime;
        

        [Inject]
        private void Construct(GameSettings gameSettings, SavedDataStorage savedDataStorage)
        {
            _dayTimeInSeconds = gameSettings.DayTimeInSeconds;
            _eveningTime = gameSettings.DurationOfDayTime / 2;
            _nightTime =  gameSettings.DurationOfDayTime;
            
            savedDataStorage.Add(this);
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
            if ( _currentSecondsOfDay < _eveningTime && CurrentTimeOfDay != TimeOfDay.Morning)
            {
                SetCurrentTimeOfDay(TimeOfDay.Morning);
            }
            else if (_currentSecondsOfDay > _eveningTime && _currentSecondsOfDay < _nightTime && CurrentTimeOfDay != TimeOfDay.Evening)
            {
                SetCurrentTimeOfDay(TimeOfDay.Evening);
            }
            else if (_currentSecondsOfDay > _nightTime && CurrentTimeOfDay != TimeOfDay.Night)
            {
                SetCurrentTimeOfDay(TimeOfDay.Night);
            }
        }

        private void SetCurrentTimeOfDay(TimeOfDay newTimeOfDay)
        {
            if (newTimeOfDay == CurrentTimeOfDay) return;

            CurrentTimeOfDay = newTimeOfDay;

            switch (CurrentTimeOfDay)
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