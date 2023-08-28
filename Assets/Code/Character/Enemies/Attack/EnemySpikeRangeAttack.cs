using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Data.GameData;
using Code.Logic.Objects.Spikes;
using Code.Services;
using Code.Services.EventsSubscribes;
using UnityEngine;

namespace Code.Character.Enemies
{
    public class EnemySpikeRangeAttack : EnemyRangeAttackBase, IEventsSubscriber
    {
        [SerializeField] private MissileSpikeController _spikeController;

        private readonly Cooldown _attackCooldown = new();
        private IEnemyStats _enemyStats;
        private EnemyAnimator _animator;
        private IHero _hero;

        public bool IsAttacking;

        public void Init(IHero hero,IEnemyStats enemyStats,SpikeAttackData data,EnemyAnimator enemyAnimator)
        {
            _hero = hero;
            _enemyStats = enemyStats;
            
            _animator = enemyAnimator;
            _animator.SetRangeAttackAnimationSpeed(data.AnimationSpeed);
            
            _attackCooldown.SetMaxTime(data.Cooldown);
        }


        public void SubscribeToEvents(bool flag)
        {
            if (flag)
            {
                _animator.Events.OnRangeAttack += OnRangeAttack;
                _animator.Events.OnRangeAttackEnded += OnRangeAttackEnded;
            }
            else
            {
                _animator.Events.OnRangeAttack -= OnRangeAttack;
                _animator.Events.OnRangeAttackEnded -= OnRangeAttackEnded;
            }
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

        private bool CanAttack() => IsActive && _attackCooldown.IsUp() && !IsAttacking
                                    &&_hero.Stats.CurrentHeath > 0 && _hero.Stats.OnGround 
                                    && !_enemyStats.IsBlock;
    }
      
}