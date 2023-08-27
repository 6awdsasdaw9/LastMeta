using System;
using Code.Debugers;
using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class StartStopAnimation: MonoBehaviour
    {
        private readonly int _stop_t = Animator.StringToHash("Stop");
        private readonly int _stopIdle_t = Animator.StringToHash("StopIdle");
        [SerializeField] protected Animator _animator;
        
        private readonly int _start_t = Animator.StringToHash("Start");
        private readonly int _startIdle_t = Animator.StringToHash("StartIdle");

        public event Action OnEndAnimation;
        private bool _isDestoy;

        
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
        public void PlayStopIdle()
        {
            if(_isDestoy)return;
            _animator?.SetTrigger(_stopIdle_t);
        }

        public void PlayStop()
        {
            if(_isDestoy)return;
            _animator?.SetTrigger(_stop_t);
        }
    }
}