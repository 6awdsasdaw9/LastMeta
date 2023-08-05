using System;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Infrastructure.GlobalEvents;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    public class BlackHandFacade : Enemy
    {
        public EnemyType Type;
        public EnemyMelleAttack MelleAttack;
        public EnemyMovementPatrol Patrol;
        public EnemyMovementToHero MovementToHero;
        public EnemyHealth Health;

        private EnemyData _data;
        private IHero _hero;

        [Inject]
        private void Construct(EnemiesConfig enemiesConfig, IHero hero, EventsFacade eventsFacade)
        {
            _data = enemiesConfig.GetByType(Type);
            _hero = hero;
            
            if (_hero == null || _data == null) return;

            MelleAttack.Init(_hero, _data.DamageParam, _data.PushData);
            Patrol.Init(_data.PatrolSpeed, _data.PatrolCooldown);
            MovementToHero.Init(_hero.Transform, _data.MoveSpeed);
            Health.Set(_data.HealthData);
        }
        
    }
}