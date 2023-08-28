using System;
using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class StartAnimation: MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        [SerializeField] private bool _isRepeat;
        
        private readonly int _start_t = Animator.StringToHash("Start");
        private readonly int _startIdle_t = Animator.StringToHash("StartIdle");
        private static readonly int _repeat_b = Animator.StringToHash("Repeat");
        public event Action OnEndAnimation;
        private bool _isDestoy;

        private void OnEnable()
        {
           _animator.SetBool(_repeat_b,_isRepeat);
        }
        
        private void OnDestroy()
        {
            _isDestoy = true;
        }

        public void PlayStart(Action OnEndAnimationEvent = null)
        {
            if (_isDestoy)
            {
                return;
            }
            OnEndAnimation = OnEndAnimationEvent;
            _animator?.SetTrigger(_start_t);
        }
        
        public void PlayStartIdle()
        {
            if (_isDestoy)
            {
                return;
            }
            _animator?.SetTrigger(_startIdle_t);
        }

        /// <summary>
        /// Animation event
        /// </summary>
        private void StartAnimation_PlayEndEvent()
        {
            OnEndAnimation?.Invoke();
        }
    }
}