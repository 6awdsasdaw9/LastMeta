using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour
    {

        [SerializeField] private HeroAnimator _animator;
       
        [Inject]
        private void Construct(InputService inputService)
        {
            inputService.PlayerAttackEvent += Attack;
        }

        private void Attack()
        {
            _animator.PlayAttack();
        }
    }
}