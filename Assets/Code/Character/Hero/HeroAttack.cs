using Code.Character.Interfaces;
using Code.Data.Configs;
using Code.Data.States;
using Code.Debugers;
using Code.Services.Input;
using UnityEngine;
using Zenject;

namespace Code.Character.Hero
{
    public class HeroAttack : MonoBehaviour, IHeroAttack
    {
        private IHero _hero;
        private InputService _inputService;
        private PowerData _power;

        private readonly Collider[] _hits = new Collider[7];
        private bool _attackIsActive;
        private int _layerMask;

        [Inject]
        private void Construct(InputService inputService, GameConfig gameConfig)
        {
            _hero = GetComponent<IHero>();
            _inputService = inputService;
            _power = gameConfig.heroConfig.power;
            _layerMask = 1 << LayerMask.NameToLayer(Constants.HittableLayer);
        }

        private void OnEnable()
        {
            _inputService.PlayerAttackEvent += Attack;
        }

        private void OnDisable()
        {
            _inputService.PlayerAttackEvent -= Attack;
        }

        public void Attack()
        {
            if (_attackIsActive || !_hero.Collision.OnGround)
                return;

            _attackIsActive = true;
            _hero.Animator.PlayAttack();
            _hero.Movement.BlockMovement(unblockCondition: !_attackIsActive);
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        public void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _power.damagedRadius, 1.0f);
            for (int i = 0; i < Hit(); ++i)
                _hits[i].GetComponent<IHealth>().TakeDamage(_power.damage);
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        public void OnAttackEnded()
        {
            _attackIsActive = false;
        }
        
        private int Hit() =>
            Physics.OverlapSphereNonAlloc(StartPoint(), _power.damagedRadius, _hits, _layerMask);
        
        private Vector3 StartPoint() =>
            new(transform.position.x + transform.localScale.x * 0.1f, transform.position.y + 0.7f,
                transform.position.z);
        
        
        public void Disable() => 
            enabled = false;
        
        public void Enable() => 
            enabled = true;
    }
}