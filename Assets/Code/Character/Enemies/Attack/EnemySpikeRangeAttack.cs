using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Debugers;
using Code.Logic.Objects.Spikes;
using Code.Services;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies
{
    public class EnemySpikeRangeAttack : EnemyRangeAttackBase
    {
        [SerializeField] private EnemyAnimator _animator;
        [SerializeField] private MissileSpikeController _spikeController;
        private IEnemyStats _enemyStats;
        private Cooldown _attackCooldown;

        private IHero _hero;

        public bool IsAttacking;

        [Inject]
        private void Construct(IHero hero)
        {
            _hero = hero;
        }
        private void Update()
        {
            if (CanAttack()) StartRangeAttack();
        }
        private bool CanAttack() => IsActive && !IsAttacking;
        protected override void StartRangeAttack()
        {
            Logg.ColorLog("StartRangeAttack",ColorType.Red);
            IsAttacking = true;
            _animator.PlayRangeAttack();
        }

        protected override void OnRangeAttack()
        {
            _spikeController.StartReaction(_hero.Transform.position);
        }

        protected override void OnRangeAttackEnded()
        {
            IsAttacking = false;
            _spikeController.EndReaction();
        }
    }
}