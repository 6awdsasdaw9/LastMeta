using UnityEngine;

namespace Code.Logic
{
    public class InteractiveIconAnimation : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        private readonly int void_t = Animator.StringToHash("Void");
        private readonly int exclamation_t = Animator.StringToHash("Exclamation");//!
        private readonly int interaction_t = Animator.StringToHash("Interaction");
        private readonly int question_t = Animator.StringToHash("Question");
        private readonly int shop_t = Animator.StringToHash("Shop");

        public void PlayVoid() => _animator.SetTrigger(void_t);
        public void PlayExclamation() => _animator.SetTrigger(exclamation_t);
        public void PlayInteraction() => _animator.SetTrigger(interaction_t);
        public void PlayQuestion() => _animator.SetTrigger(question_t);
        public void PlayShop() => _animator.SetTrigger(shop_t);
        public void PlayType(InteractiveType type) => _animator.SetTrigger(type.ToString());
    }
}