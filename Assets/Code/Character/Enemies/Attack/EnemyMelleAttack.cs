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
        [SerializeField] private EnemyAnimator _animator;
        private Cooldown _attackCooldown;
        private HeroPusher _pusher;
        
        private DamageParam _damageParam;
        
        private IHero _hero;
        private Vector3 _startPoint;
        private readonly Collider[] _hits = new Collider[1];

        private int _layerMask;
        public bool IsAttacking;
        private IEnemyStats _enemyStats;
        
        public void Init(IHero hero, DamageParam damageParam, PushData pushData, IEnemyStats enemyStats)
        {
            _hero = hero;
            _damageParam =  damageParam;
            _enemyStats = enemyStats;
            
            _attackCooldown = new Cooldown();
            _attackCooldown.SetTime(_damageParam.Cooldown);

            _pusher = new HeroPusher(transform, hero, pushData);
            
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
            PhysicsDebug.DrawDebug(StartPoint(), _damageParam.DamagedRadius, 1);
            if (!Hit(out Collider hit)) return;
            _hero.Health.TakeDamage(_damageParam.Damage);
            _pusher.Push().Forget();
        }

        /// <summary> 
        /// Animation Event
        /// </summary>
        protected  override void OnAttackEnded()
        {
            _attackCooldown.ResetCooldown();
            IsAttacking = false;
        }
   


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