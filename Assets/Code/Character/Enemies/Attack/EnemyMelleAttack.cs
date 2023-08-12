using System.Linq;
using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Common;
using Code.Services;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyMelleAttack : EnemyMelleAttackBase
    {
        private EnemyAnimator _animator;
        private readonly Cooldown _attackCooldown = new();
        
        private IEnemyStats _enemyStats;
        private IHero _hero;
        
        private AttackData _attackData;
        private PushData _pushData;
        
        private Vector3 _attackPoint;
        private readonly Collider[] _hits = new Collider[1];

        private int _layerMask;
        public bool IsAttacking { get; private set; }

        public void Init(IHero hero, AttackData attackData, PushData pushData, IEnemyStats enemyStats, EnemyAnimator animator)
        {
            _hero = hero;
            _attackData =  attackData;
            _pushData = pushData;
            _enemyStats = enemyStats;

            _animator = animator;
            _animator.SetMelleAttackAnimationSpeed(attackData.AnimationSpeed);
            
            _attackCooldown.SetMaxTime(_attackData.Cooldown);
            _layerMask =  LayerMask.GetMask(Constants.HeroLayer);
        }
        
        private void Update()
        {
            if (CanAttack()) StartAttack();
        }

        private void OnDisable()
        {
            if (IsAttacking)
            {
                _hero.Movement.SetSupportVelocity(Vector2.zero);
            }
            _attackCooldown.SetMaxCooldown();
        }
        protected override void StartAttack()
        {
            IsAttacking = true;
            _animator.PlayMelleAttack();
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        protected  override void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _attackData.DamagedRadius, 1);
            if (!Hit(out Collider hit)) return;
            _hero.Health.TakeDamage(_attackData.Damage);
            _hero.EffectsController.Push(forward: (transform.position - _hero.Transform.position) * _pushData.Force);
        }

        /// <summary> 
        /// Animation Event
        /// </summary>
        protected  override void OnAttackEnded()
        {
            _attackCooldown.SetMaxCooldown();
            IsAttacking = false;
        }
        

        private bool Hit(out Collider hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _attackData.DamagedRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() => transform.position + _attackData.EffectiveDistance;
        private bool CanAttack() => IsActive && !IsAttacking && _attackCooldown.IsUp() && !_enemyStats.IsBlock && _hero.Health.Current > 0;
    }
}