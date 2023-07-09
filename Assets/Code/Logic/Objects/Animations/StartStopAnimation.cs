using Code.Logic.Objects.Animations;
using UnityEngine;

namespace Code.PresentationModel.HudElements
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