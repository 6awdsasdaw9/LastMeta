using UnityEngine;

namespace Code.Logic.Objects.Animations
{
    public class DestroyedAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _destroy = Animator.StringToHash("Destroy");

        public void SetAnimatorController(RuntimeAnimatorController animatorController) => 
            _animator.runtimeAnimatorController = animatorController;

        public void PlayDestroy() => 
            _animator.SetTrigger(_destroy);

        public void DisableAnimation()
        {
            _animator.enabled = false;
        }
    }
}