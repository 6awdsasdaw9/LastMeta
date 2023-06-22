using UnityEngine;

namespace Code.Logic.Objects
{
    public class DestroyedObjectAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int _destroy = Animator.StringToHash("Destroy");

        public void PlayDestroy()
        {
            _animator.SetTrigger(_destroy);
        }
    }
}