using Code.Character.Common;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Character.Enemies;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Services.Input;
using UnityEngine;
using Zenject;
using HeroConfig = Code.Data.Configs.HeroConfig;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour, IHeroAttack
    {
        private IHero _hero;
        private InputService _inputService;
        private RaycastHits _raycastHit;
        public DamageParam DamageParam { get; private set; }
        public bool IsAttack { get; private set; }
        private bool _isCanAttack => !_hero.Stats.IsDash 
                                    && !_hero.Stats.IsCrouch 
                                    && !_hero.Stats.IsJump
                                    && !_hero.Stats.IsBlockMove;

        [Inject]
        private void Construct(InputService inputService, HeroConfig heroConfig)
        {
            _hero = GetComponent<IHero>();
        
            _raycastHit = new RaycastHits(transform, Constants.HittableLayer, 0.2f, hitsSize: 7);
            _inputService = inputService;
        }
        public void Enable() => enabled = true;
        private void OnEnable() => _inputService.OnPressAttackButton += StartAttack;
        public void Disable() => enabled = false;
        private void OnDisable() => _inputService.OnPressAttackButton -= StartAttack;
        public void SetDamageParam(DamageParam damageParam) => DamageParam = damageParam;
        private void StartAttack()
        {
            if (IsAttack || !_isCanAttack)
                return;

            IsAttack = true;
            _hero.Animator.PlayAttack();
            _hero.Movement.BlockMovement();
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        public void OnAttack()
        {
           var damageTakers = _raycastHit.GetComponents<IHealth>();

           foreach (var health in damageTakers)
           {
               health.TakeDamage(_hero.Stats.Damage);
           }
               
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        public void OnAttackEnded()
        {
            IsAttack = false;
            _hero.Movement.UnBlockMovement();
        }
    }
}