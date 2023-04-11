using UnityEngine;

namespace Code.Logic.Interactive.InteractiveObjects
{
    public class InteractiveObjectAnimation: Interactivity
    {
        [SerializeField] private Animator _animator;
        private readonly int Start_t = Animator.StringToHash("Start");
        private readonly int End_t = Animator.StringToHash("End");
        
        public override void StartInteractive()
        {
            _animator.SetTrigger(Start_t);
            OnStartInteractive?.Invoke();
        }

        public override void StopInteractive()
        {
            _animator.SetTrigger(End_t);
            OnEndInteractive?.Invoke();
        }
    }
}