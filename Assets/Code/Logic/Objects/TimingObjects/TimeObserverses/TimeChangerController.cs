using System.Collections.Generic;
using Code.Data.GameData;
using Code.Infrastructure.GlobalEvents;
using Code.Logic.Objects.TimingObjects.TimeObserverses.Interfaces;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.TimingObjects.TimeObserverses
{
    public class TimeChangerController : MonoBehaviour, IEventSubscriber,ITimeObserver
    {
        [SerializeField] private List<TimeObserver> _timeObservers;
        
        private EventsFacade _eventsFacade;

        private bool _isEmptyController => _timeObservers.Count == 0;
        [Inject]
        private void Construct(EventsFacade eventsFacade)
        {
            _eventsFacade = eventsFacade;
        }

        private void OnEnable()
        {
            SubscribeToEvent(true);
        }

        private void OnDisable()
        {
            SubscribeToEvent(false);
        }

        public void SubscribeToEvent(bool flag)
        {
            if(_isEmptyController)
                return;
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

        public void OnLoadScene()
        {
            foreach (var timeObserver in _timeObservers)
            {
                timeObserver.OnLoadScene();
            }
        }

        public void OnStartTimeOfDay(TimeOfDay timeOfDay)
        {
            foreach (var timeObserver in _timeObservers)
            {
                timeObserver.OnStartTimeOfDay(timeOfDay);
            }
        }
    }
}