using System;
using Code.Logic.Collisions.Triggers;
using Code.Logic.TimingObjects.TimeObserverses;
using UnityEngine;

namespace Code.Logic.Toys
{
    public class ToyController : TimeObserver
    {
        [SerializeField] private TriggerObserver _trigger;
        public Action OnEndAnimation; 

        protected override void StartReaction()
        {
            ListenTrigger(true);
        }

        protected override void EndReaction()
        {
            ListenTrigger(false);
        }

        protected override void SetStartReaction()
        {
            ListenTrigger(true);
        }

        protected override void SetEndReaction()
        {
        }

        private void ListenTrigger(bool flag)
        {
            if (flag)
            {
                _trigger.OnEnter += OnEnter;
            }
            else
            {
                _trigger.OnEnter -= OnEnter;
            }
        }

        private void OnEnter(GameObject obj)
        {
            
        }
    }
}