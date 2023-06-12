using System;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Services.SaveServices;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class GameClock : ITickable, ISavedData
    {
        private readonly float _dayTimeInSeconds;
        private readonly float  _eveningTime;
        private readonly float  _nightTime;

        public TimeData CurrentTime { get; private set; } = new TimeData();
        public float DurationOfDayTime { get; private set; }
        public float DayTimeNormalized => CurrentTime.Seconds / _dayTimeInSeconds;
     
        
        public bool IsMorningTime => CurrentTime.Seconds < _eveningTime;
        public  bool IsEveningTime => CurrentTime.Seconds > _eveningTime 
                                      && CurrentTime.Seconds < _nightTime;
        public bool IsNightTime => CurrentTime.Seconds > _nightTime;


        public GameClock(
            GameSettings gameSettings)
        {
            DurationOfDayTime = gameSettings.DurationOfDayTime;
            _dayTimeInSeconds = gameSettings.DayTimeInSeconds;
            _eveningTime = gameSettings.DurationOfDayTime / 2;
            _nightTime =  gameSettings.DurationOfDayTime;
            
           // savedDataStorage.Add(this);
        }

        public void Tick()
        {
            ClockMovement();
           // CheckTimeOfDay();
        }

        private void ClockMovement()
        {
            CurrentTime.Seconds += Time.deltaTime;
            if (CurrentTime.Seconds >= _dayTimeInSeconds)
            {
                CurrentTime.Seconds = 0;
                CurrentTime.Day++;
            }
        }

        /*
        private void CheckTimeOfDay()
        {
            if ( IsMorningTime && CurrentTimeOfDay !=  Constants.TimeOfDay.Morning)
            {
                SetCurrentTimeOfDay( Constants.TimeOfDay.Morning);
            }
            else if (IsEveningTime && CurrentTimeOfDay !=  Constants.TimeOfDay.Evening)
            {
                SetCurrentTimeOfDay( Constants.TimeOfDay.Evening);
            }
            else
            {
           
                if (IsNightTime && CurrentTimeOfDay !=  Constants.TimeOfDay.Night)
                {
                    SetCurrentTimeOfDay( Constants.TimeOfDay.Night);
                }
            }
        }

        private void SetCurrentTimeOfDay( Constants.TimeOfDay newTimeOfDay)
        {
            if (newTimeOfDay == CurrentTimeOfDay) 
                return;

            CurrentTimeOfDay = newTimeOfDay;

            switch (CurrentTimeOfDay)
            {
                case  Constants.TimeOfDay.Morning:
                    OnMorning?.Invoke();
                    break;
                case  Constants.TimeOfDay.Evening:
                    OnEvening?.Invoke();
                    break;
                default:
                case  Constants.TimeOfDay.Night:
                    OnNight?.Invoke();
                    break;
            }
        }
        */

     
        public void LoadData(SavedData savedData)
        {
            CurrentTime = savedData.TimeData;
        }

        public void SaveData(SavedData savedData)
        {
            savedData.TimeData = CurrentTime;
        }
    }

  
}