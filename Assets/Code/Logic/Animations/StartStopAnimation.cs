using System;
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
        public event Action OnStartAnimationPlayed;
        public event Action OnStopAnimationPlayed;
        
        private bool _isDestoy;

        
        private void OnDestroy()
        {
            _isDestoy = true;
        }

        public void PlayStart(Action OnStartAnimationPlayed = null)
        {
            if (_isDestoy)
            {
                return;
            }
            this.OnStartAnimationPlayed = OnStartAnimationPlayed;
            _animator?.SetTrigger(_start_t);
        }
        
        public void PlayStartIdle(Action OnStartAnimationPlayed = null)
        {
            if (_isDestoy)
            {
                return;
            }
            _animator?.SetTrigger(_startIdle_t);
            OnStartAnimationPlayed?.Invoke();
        }
        public void PlayStopIdle(Action OnStopAnimationPlayed = null)
        {
            if(_isDestoy)return;
            _animator?.SetTrigger(_stopIdle_t);
            OnStopAnimationPlayed?.Invoke();
        }

        public void PlayStop(Action OnStopAnimationPlayed = null)
        {
            if(_isDestoy)return;
            this.OnStopAnimationPlayed = OnStopAnimationPlayed;
            _animator?.SetTrigger(_stop_t);
        }

        /// <summary>
        /// Animation event
        /// </summary>
        private void StartStopAnimation_StartAnimationPlayedEvent()
        {
            OnStartAnimationPlayed?.Invoke();
        }
        
        /// <summary>
        /// Animation event
        /// </summary>
        private void StartStopAnimation_StopAnimationPlayedEvent()
        {
            OnStopAnimationPlayed?.Invoke();
        }
        
    }
}