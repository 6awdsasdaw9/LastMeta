using Code.Logic.DayOfTime;
using UnityEngine;

namespace Code.Logic.Objects.TimingObjects
{
    public class TimeObserverAnimator : TimeObserver
    {
        [SerializeField] private Animator _animator;
        
        private readonly int _start_t = Animator.StringToHash("Start");
        private readonly int _stop_t = Animator.StringToHash("Stop");

        protected override void StartReaction()
        {
            _animator.SetTrigger(_start_t);
        }

        protected override void EndReaction()
        {
            _animator.SetTrigger(_stop_t);
        }
    }
}