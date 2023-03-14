using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour
    {

        [SerializeField] private HeroAnimator _animator;
       
        [Inject]
        private void Construct(InputController inputController)
        {
            inputController.PlayerAttackEvent += Attack;
        }

        private void Attack()
        {
            _animator.PlayAttack();
        }
    }
}