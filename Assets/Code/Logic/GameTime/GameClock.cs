using System;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Services;
using Code.Services.SaveServices;
using UnityEngine;
using Zenject;

namespace Code.Logic.DayOfTime
{
    public class GameClock : ITickable, ISavedData, IEventSubscriber
    {
        private float _dayTimeInSeconds;
        private float _eveningTime;
        private float _nightTime;

        private bool _isMove;
        
        private readonly GameSceneData _gameSceneData;
        private readonly EventsFacade _eventsFacade;

        public TimeData CurrentTime { get; private set; } = new();
     
        public float DayTimeInSeconds => _dayTimeInSeconds;
        public float DayTimeNormalized => CurrentTime.Seconds / _dayTimeInSeconds;

        public bool IsMorningTime => CurrentTime.Seconds < _eveningTime;

        public bool IsEveningTime => CurrentTime.Seconds > _eveningTime
                                     && CurrentTime.Seconds < _nightTime;

        public bool IsNightTime => CurrentTime.Seconds > _nightTime;


        public GameClock(GameSceneData gameSceneData, EventsFacade eventsFacade)
        {
            _eventsFacade = eventsFacade;
            _gameSceneData = gameSceneData;
            SubscribeToEvent(true);
        }

        public void Tick()
        {
            if (_isMove)
            {
                ClockMovement();
            }
        }

        public void SubscribeToEvent(bool flag)
        {
            if (flag)
            {
                _eventsFacade.SceneEvents.OnLoadScene += OnLoadScene;
                _eventsFacade.SceneEvents.OnStartPause += StopMove;
                _eventsFacade.SceneEvents.OnStopPause += StartMove;
            }
            else
            {
                _eventsFacade.SceneEvents.OnLoadScene -= OnLoadScene;
                _eventsFacade.SceneEvents.OnStartPause -= StopMove;
                _eventsFacade.SceneEvents.OnStopPause -= StartMove;
            }
        }

        public void SetTimeOfDay(TimeOfDay timeOfDay)
        {
            CurrentTime.TimeOfDay = timeOfDay.ToString();
        }

        private void OnLoadScene()
        {
            SetDayDuration();
            StartMove();
        }

        private void StartMove()
        {
            _isMove = true;
        }

        private void StopMove()
        {
            _isMove = false;
        }

        private void SetDayDuration()
        {
            _dayTimeInSeconds = _gameSceneData.ScenesConfig.DayTimeInSeconds;
            var eveningParam = _gameSceneData.CurrentSceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Evening);
            var nightParam = _gameSceneData.CurrentSceneParams.TimeOfDaySettings.GetLightParams(TimeOfDay.Night);

            _eveningTime = Mathf.Lerp(0, _dayTimeInSeconds, eveningParam.Duration);
            _nightTime = Mathf.Lerp(0, _dayTimeInSeconds, nightParam.Duration);
            Logg.ColorLog($"Duration = {_dayTimeInSeconds}, evening = {_eveningTime}, night = {_nightTime}");
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

        public void LoadData(SavedData savedData)
        {
            Logg.ColorLog($"savedData.TimeData == null {savedData.TimeData == null}");
            if (savedData.TimeData == null)
                return;
            Logg.ColorLog(
                $"s {savedData.TimeData.Seconds} d {savedData.TimeData.Day}  tod{savedData.TimeData.TimeOfDay} ");
            CurrentTime = new TimeData()
            {
                TimeOfDay = savedData.TimeData.TimeOfDay,
                Seconds = savedData.TimeData.Seconds,
                Day = savedData.TimeData.Day
            };
        }

        public void SaveData(SavedData savedData)
        {
            savedData.TimeData = CurrentTime;
        }
    }
}