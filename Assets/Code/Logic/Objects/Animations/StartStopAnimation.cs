using System;
using Code.Debugers;
using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class StartStopAnimation: StartAnimation
    {
        private readonly int _stop_t = Animator.StringToHash("Stop");
        private readonly int _stopIdle_t = Animator.StringToHash("StopIdle");

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