using UnityEngine;

namespace Code.Logic.DayOfTime
{
   
    public class AnimationTimeObserver : TimeObserver
    {
        [SerializeField] private Animator _animator;
        private GameClock _gameClock;
        private readonly int _start = Animator.StringToHash("StartTimeReaction");
        private readonly int _end = Animator.StringToHash("EndTimeReaction");
        
        
        protected override void StartReaction()
        {
            _animator.SetTrigger(_start);
        }

        protected override void EndReaction()
        {
            _animator.SetTrigger(_end);
        }
    }
}