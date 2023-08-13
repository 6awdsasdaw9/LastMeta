using Code.Character.Enemies;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Common;
using Code.Logic.Common.Interfaces;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour, IHeroAttack
    {
        [SerializeField] private Hero _hero;
        private InputService _inputService;
        private RaycastHitsController _raycastHitController;
        public AttackData AttackData { get; private set; }
        public bool IsAttack { get; private set; }

        private bool _isCanAttack => !_hero.Stats.IsDash
                                     && _hero.Stats.OnGround
                                     && !_hero.Stats.IsCrouch
                                     && !_hero.Stats.IsBlockMove;

        [Inject]
        private void Construct(InputService inputService)
        {
            _inputService = inputService;
        }
        

        public void EnableComponent()
        {
            if (this != null) enabled = true;
        }

        private void OnEnable() => _inputService.OnPressAttackButton += StartAttack;

        public void DisableComponent()
        {
            if (this != null) enabled = false;
        }

        private void OnDisable() => _inputService.OnPressAttackButton -= StartAttack;

        public void SetDamageParam(AttackData attackData)
        {
            AttackData = attackData;
            _raycastHitController = new RaycastHitsController(
                owner: _hero.Transform,
                layerName: Constants.HittableLayer,
                hitRadius: 0.2f,
                hitsSize: 7,
                hitOffsetX: attackData.EffectiveDistance.x,
                hitOffsetY: attackData.EffectiveDistance.y);
        }

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
            var damageTakers = _raycastHitController.GetComponents<IHealth>();

            foreach (var health in damageTakers)
            {
                health.TakeDamage(AttackData.Damage);
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