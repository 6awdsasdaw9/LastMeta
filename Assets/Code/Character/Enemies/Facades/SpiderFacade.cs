using System;
using Code.Character.Enemies.EnemiesInterfaces;
using Code.Logic.Common;
using Code.Logic.Common.Interfaces;
using UnityEngine;

namespace Code.Character.Enemies.EnemiesFacades
{
    [RequireComponent(typeof(AgentRotateToForfard),typeof(EnemyMovementPatrol),typeof(EnemyCollisionAttack))]
    public class SpiderFacade : EnemyFacade
    {
        public AgentRotateToForfard RotateToForward;
        public EnemyMovementPatrol Patrol;
        public EnemyCollisionAttack CollisionAttack;
        public VulnerableZone VulnerableZone;
        
        protected override void InitComponents()
        {
            Stats = new SpiderStats(this);
            EnemyAudio.Init(data.AudioPath);
            Health.Set(data.HealthData);
            CollisionAttack.Init(hero, data.CollisionAttackData, collisionAttackDamage);
            Patrol.Init(data.PatrolSpeed, data.PatrolCooldown, Stats);
        }

        public override void OnPause()
        {
            Stats.Block();
        }

        public override void OnResume()
        {
            Stats.UnBlock();
        }

        protected override void OnValidate()
        {
            base.OnValidate();
            RotateToForward = GetComponent<AgentRotateToForfard>();
            Patrol = GetComponent<EnemyMovementPatrol>();
            CollisionAttack = GetComponent<EnemyCollisionAttack>();
            VulnerableZone = GetComponent<VulnerableZone>();
        }
    }

    public class SpiderStats : IEnemyStats
    {
        private readonly SpiderFacade _spiderFacade;
        public bool IsLoockLeft { get; private set; }

        public bool IsBlock { get; private set; }
        public bool IsPatroling => _spiderFacade.Patrol.IsMoving;

        public bool IsMovingToHero => false;

        public bool IsAttacking => false;

        public bool IsMelleAttacking => false;

        public bool IsRangeAttacing => false;


        public SpiderStats(SpiderFacade spiderFacade)
        {
            _spiderFacade = spiderFacade;
        }

        public void Block()
        {
            if (IsBlock)
            {
                return;
            }

            IsBlock = true;
            _spiderFacade.Patrol.DisableComponent();
        }

        public void UnBlock()
        {
            if (!IsBlock)
            {
                return;
            }

            IsBlock = false;
            _spiderFacade.Patrol.EnableComponent();
        }

        public void SetLoockLeft(bool isLoockLeft)
        {
            IsLoockLeft = isLoockLeft;
        }

        public void SubscribeToEvents(bool flag)
        {
            if (_spiderFacade == null) return;

            if (flag)
            {
                _spiderFacade.RotateToForward.OnFlipLeft += SetLoockLeft;
            }
            else
            {
                _spiderFacade.RotateToForward.OnFlipLeft -= SetLoockLeft;
            }
        }
    }
}