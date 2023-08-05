using Code.Character.Common;
using Code.Character.Common.CommonCharacterInterfaces;
using Code.Character.Enemies;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Debugers;
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
        public DamageParam DamageParam { get; private set; }
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

        public void SetDamageParam(DamageParam damageParam)
        {
            DamageParam = damageParam;
            _raycastHitController = new RaycastHitsController(
                owner: transform,
                layerName: Constants.HittableLayer,
                hitRadius: 0.2f,
                hitsSize: 7,
                hitOffsetX: damageParam.EffectiveDistance.x,
                hitOffsetY: damageParam.EffectiveDistance.y);
            Logg.ColorLog($"Hero attack damage = {damageParam.Damage}");
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
                health.TakeDamage(DamageParam.Damage);
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