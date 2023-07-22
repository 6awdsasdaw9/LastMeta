using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class StartStopAnimation: StartAnimation
    {
        private readonly int _stop_t = Animator.StringToHash("Stop");
        
        public void PlayStop()
        {
            _animator.SetTrigger(_stop_t);
        }
    }
}