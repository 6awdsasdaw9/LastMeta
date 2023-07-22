using Code.Data.GameData;
using Code.Logic.DayOfTime;
using Code.Logic.Objects.TimingObjects.TimeObserverses.Interfaces;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Logic.Objects.TimingObjects.TimeObserverses
{
    [RequireComponent(typeof(TimeChangerController))]
    public abstract class  TimeObserver: MonoBehaviour, ITimeObserver
    {
        [SerializeField,EnumToggleButtons] protected TimeOfDay _timeToEnable = TimeOfDay.Night;
        [SerializeField, EnumToggleButtons] protected TimeOfDay _timeToDisable= TimeOfDay.Morning;
        private GameClock _gameClock;

        [Inject]
        private void Construct(GameClock gameClock)
        {
            _gameClock = gameClock;
        }
        public  void OnLoadScene()
        {
            if (_gameClock.CurrentTime.TimeOfDay == _timeToEnable.ToString())
            {
                StartReaction();
            }
            else
            {
                EndReaction();
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
        
    }
}