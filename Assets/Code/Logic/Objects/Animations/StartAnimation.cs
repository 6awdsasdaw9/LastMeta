using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class StartAnimation: MonoBehaviour
    {
        [SerializeField] protected Animator _animator;
        
        private readonly int _start_t = Animator.StringToHash("Start");

        public void PlayStart()
        {
            _animator.SetTrigger(_start_t);
        }

    }
}