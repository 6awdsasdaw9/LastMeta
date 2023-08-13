using System;
using Code.Debugers;
using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class StartAnimation: MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        private readonly int _start_t = Animator.StringToHash("Start");
        private readonly int _startIdle_t = Animator.StringToHash("StartIdle");
        protected bool _isDestoy;

        private void OnDestroy()
        {
            _isDestoy = true;
        }

        public void PlayStart()
        {
            if(_isDestoy)return;
            _animator?.SetTrigger(_start_t);
        }
        
        public void PlayStartIdle()
        {
            if(_isDestoy)return;
            _animator?.SetTrigger(_startIdle_t);
        }

    }
}