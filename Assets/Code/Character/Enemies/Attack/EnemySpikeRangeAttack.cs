using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Logic.Objects.Spikes;
using Code.Services;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemySpikeRangeAttack : EnemyRangeAttackBase
    {
        [SerializeField] private MissileSpikeController _spikeController;

        private Cooldown _attackCooldown;
        private IEnemyStats _enemyStats;
        private EnemyAnimator _animator;
        private IHero _hero;

        public bool IsAttacking;

        public void Init(IHero hero,IEnemyStats enemyStats,EnemyAnimator enemyAnimator,float cooldown)
        {
            _hero = hero;
            _animator = enemyAnimator;
            _enemyStats = enemyStats;
            _attackCooldown = new Cooldown();
            _attackCooldown.SetTime(cooldown);
        }

        private void Update()
        {
            if (CanAttack()) StartRangeAttack();
        }

        protected override void StartRangeAttack()
        {
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

        private bool CanAttack() => IsActive && !IsAttacking && _attackCooldown.IsUp()
                                    &&_hero.Stats.CurrentHeath > 0 && _hero.Stats.OnGround 
                                    && !_enemyStats.IsBlock;
    }
}