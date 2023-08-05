using System;
using System.Linq;
using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Collisions.Triggers;
using Code.Logic.Common;
using Code.Services;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyMelleAttack : FollowTriggerObserver
    {
        [SerializeField] private EnemyAnimator _animator;
        private Cooldown _attackCooldown;
        
        private DamageParam _damageParam;
        private PushData _pushData;
        
        private IHero _hero;
        private Vector3 _startPoint;
        private readonly Collider[] _hits = new Collider[1];

        private int _layerMask;
        public bool IsAttacking;
        private IEnemyStats _enemyStats;
        private bool IsActive { get; set; }
        
        public void Init(IHero hero, DamageParam damageParam, PushData pushData, IEnemyStats enemyStats)
        {
            _hero = hero;
            _damageParam =  damageParam;
            _pushData = pushData;
            _enemyStats = enemyStats;
            
            _attackCooldown = new Cooldown();
            _attackCooldown.SetTime(_damageParam.Cooldown);
            
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
            _attackCooldown.ResetCooldown();
        }

        private void StartAttack()
        {
            IsAttacking = true;
            _animator.PlayMelleAttack();
        }

        /// <summary>
        /// Animation Event
        /// </summary>
        private void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _damageParam.DamagedRadius, 1);
            if (!Hit(out Collider hit)) return;
            _hero.Health.TakeDamage(_damageParam.Damage);
            Push().Forget();
        }

        private async UniTaskVoid Push()
        {
            if (_pushData.Force == 0) return;
            _hero.Movement.SetSupportVelocity((transform.position - _hero.Transform.position) * _pushData.Force);
            await UniTask.Delay(TimeSpan.FromSeconds(_pushData.Duration));
            _hero.Movement.SetSupportVelocity(Vector2.zero);
        }

        /// <summary> 
        /// Animation Event
        /// </summary>
        private void OnAttackEnded()
        {
            _attackCooldown.ResetCooldown();
            IsAttacking = false;
        }

        public void DisableAttack() => IsActive = false;
        public void EnableAttack() => IsActive = true;

        private bool Hit(out Collider hit)
        {
            var hitCount = Physics.OverlapSphereNonAlloc(StartPoint(), _damageParam.DamagedRadius, _hits, _layerMask);
            hit = _hits.FirstOrDefault();
            return hitCount > 0;
        }

        private Vector3 StartPoint() => transform.position + _damageParam.EffectiveDistance;
        private bool CanAttack() => IsActive && !IsAttacking && _attackCooldown.IsUp() && !_enemyStats.IsBlock;
    }
}