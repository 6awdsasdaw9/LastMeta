using UnityEngine;

namespace Code.PresentationModel.HudElements
{
    public class StartStopAnimation: MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private readonly int _start_t = Animator.StringToHash("Start");
        private readonly int _stop_t = Animator.StringToHash("Stop");

        public void PlayStart()
        {
            _animator.SetTrigger(_start_t);
        }

        public void PlayStop()
        {
            _animator.SetTrigger(_stop_t);
        }
    }
}