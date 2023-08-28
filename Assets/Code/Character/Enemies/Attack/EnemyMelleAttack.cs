using System;
using System.Linq;
using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.GameData;
using Code.Debugers;
using Code.Logic.Common;
using Code.Services;
using Code.Services.EventsSubscribes;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Code.Character.Enemies
{
    [RequireComponent(typeof(EnemyAnimator))]
    public class EnemyMelleAttack : EnemyMelleAttackBase, IEventsSubscriber
    {
        private EnemyAnimator _animator;
        private IEnemyStats _enemyStats;
        private IHero _hero;
        private AttackData _attackData;

        private Vector3 _attackPoint;
        private int _layerMask;
        private readonly Collider[] _hits = new Collider[1];

        [ShowInInspector, ReadOnly] public bool IsAttacking { get; private set; }

        [ShowInInspector, ReadOnly] private readonly Cooldown _attackCooldown = new();

        public void Init(IHero hero, AttackData attackData, IEnemyStats enemyStats, EnemyAnimator animator)
        {
            _hero = hero;
            _attackData = attackData;
            _enemyStats = enemyStats;

            _animator = animator;
            _animator.SetMelleAttackAnimationSpeed(attackData.AnimationSpeed);

            _attackCooldown.SetMaxTime(_attackData.Cooldown);
            _layerMask = LayerMask.GetMask(Constants.HeroLayer);
            Logg.ColorLog("EnemyMelleAttack: Init");
        }


        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _animator.Events.OnAttack += OnAttack;
                _animator.Events.OnAttackEnded += OnAttackEnded;
            }
            else
            {
                _animator.Events.OnAttack -= OnAttack;
                _animator.Events.OnAttackEnded -= OnAttackEnded;
            }
        }


        private void Update()
        {
            if (CanAttack())
            {
                StartAttack();
            }
        }

        protected override void StartAttack()
        {
            IsAttacking = true;
            _animator.PlayMelleAttack();
        }


        protected override void OnAttack()
        {
            PhysicsDebug.DrawDebug(StartPoint(), _attackData.DamagedRadius, 1);
            if (!Hit(out Collider hit))
            {
                return;
            }

            _hero.Health.TakeDamage(_attackData.Damage);
            _hero.EffectsController.Push(forward: (transform.position - _hero.Transform.position) *
                                                  _attackData.PushForce);
        }


        protected override void OnAttackEnded()
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

        private Vector3 StartPoint() => transform.position + new Vector3(
            (_enemyStats.IsLoockLeft ? -_attackData.EffectiveDistance.x : _attackData.EffectiveDistance.x),
            _attackData.EffectiveDistance.y, 0);

        private bool CanAttack() => IsActive && !IsAttacking && _attackCooldown.IsUp() && !_enemyStats.IsBlock &&
                                    _hero.Health.Current > 0;
    }
}