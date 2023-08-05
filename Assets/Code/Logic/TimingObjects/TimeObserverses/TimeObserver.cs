using System;
using Code.Data.GameData;
using Code.Debugers;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.TimingObjects.TimeObserverses.Interfaces;
using Code.Services.EventsSubscribes;
using Code.Services.GameTime;
using FMODUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.TimingObjects.TimeObserverses
{
    public abstract class  TimeObserver: MonoBehaviour, ITimeObserver, IEventsSubscriber
    {
        [SerializeField,EnumToggleButtons] protected TimeOfDay _timeToEnable = TimeOfDay.Night;
        [SerializeField, EnumToggleButtons] protected TimeOfDay _timeToDisable= TimeOfDay.Morning;
        private GameClock _gameClock;

        private EventsFacade _eventsFacade;

        [Inject]
        private void Construct(DiContainer container)
        {
            _gameClock = container.Resolve<GameClock>();
            _eventsFacade = container.Resolve<EventsFacade>();
        }

        private void OnEnable()
        {
            SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            SubscribeToEvents(false);
        }

        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _eventsFacade.SceneEvents.OnLoadScene += OnLoadScene;
                _eventsFacade.TimeEvents.OnStartTimeOfDay += OnStartTimeOfDay;
            }
            else
            {
                _eventsFacade.SceneEvents.OnLoadScene -= OnLoadScene;
                _eventsFacade.TimeEvents.OnStartTimeOfDay -= OnStartTimeOfDay;
            }
        }

        public  void OnLoadScene()
        {
            if (_gameClock.CurrentTime.TimeOfDay == _timeToEnable.ToString())
            {
                SetStartReaction();
                Logg.ColorLog("TimeObserver set start reaction");
            }
            else
            {
                SetEndReaction();
                Logg.ColorLog("TimeObserver set end reaction");
            }
        }

        public virtual void OnStartTimeOfDay(TimeOfDay timeOfDay)
        {
            if (timeOfDay == _timeToEnable)
            {
                StartReaction();
            }
            else if(timeOfDay == _timeToDisable)
            {
                EndReaction();
            }
        }

        protected abstract void StartReaction();

        protected abstract void EndReaction();

        protected abstract void SetStartReaction();

        protected abstract void SetEndReaction();
    }
}