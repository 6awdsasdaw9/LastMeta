using Code.Character.Enemies.EnemiesInterfaces;
using Code.Data.Configs;
using Code.Logic.Common;
using Code.Logic.Objects.Spikes;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    public class BlackHandFacade : EnemyFacade
    {
        [Space, Title("Black Hand components")]
        public EnemyCollisionAttack CollisionAttack;

        public EnemyMelleAttack MelleAttack;
        public EnemySpikeRangeAttack SpikeAttack;
        public MissileSpikeController MissileSpike;
        [Space] public EnemyMovementPatrol Patrol;
        public RotateToHero RotateToHero;
        public AgentRotateToForfard RotateToForward;
        [Space] public GameObject HealthBar;

        protected override void InitComponents(DiContainer container)
        {
            Stats = new BlackHandStats(this);
            RotateToHero.Init(hero);
            CollisionAttack.Init(hero, data.CollisionAttackData, collisionAttackDamage);
            MelleAttack.Init(hero, data.MelleAttackData, Stats, Animator);
            SpikeAttack.Init(hero, Stats, data.SpikeAttackData, Animator);
            MissileSpike.Init(hero, container.Resolve<ObjectsConfig>());
            Patrol.Init(data.PatrolSpeed, data.PatrolCooldown, Stats);
            EnemyAudio.Init(data.AudioPath);
            Health.Set(data.HealthData);
        }

        public override void Die()
        {
            Animator.PlayDeath();
            Stats.Block();
            HealthBar.SetActive(false);

            MissileSpike.EndReaction();

            SpikeAttack.SubscribeToEvents(false);
            MelleAttack.SubscribeToEvents(false);
            CollisionAttack.SubscribeToEvents(false);
            CollisionsController.SetActive(false);
        }

        public override void Revival()
        {
            HealthBar.SetActive(false);
            
            Animator.PlayEnter(() =>
            {
                HealthBar.SetActive(true);
                Stats.UnBlock();
                Health.Reset();

                Death.SubscribeToEvents(true);
                SpikeAttack.SubscribeToEvents(true);
                MelleAttack.SubscribeToEvents(true);
                CollisionAttack.SubscribeToEvents(true);
                CollisionsController.SetActive(true);
            });
        }

        public override void OnPause()
        {
            Stats.Block();
            Patrol.OnPause();
        }

        public override void OnResume()
        {
            Stats.UnBlock();
        }
    }

    public class BlackHandStats : IEnemyStats
    {
        private readonly BlackHandFacade _blackHandFacade;
        public bool IsLoockLeft { get; private set; }
        public bool IsPatroling => _blackHandFacade.Patrol.IsMoving;
        public bool IsMovingToHero => false;
        public bool IsAttacking => IsMelleAttacking || IsRangeAttacing;
        public bool IsMelleAttacking => _blackHandFacade.MelleAttack.IsAttacking;
        public bool IsRangeAttacing => _blackHandFacade.SpikeAttack.IsAttacking;
        public bool IsBlock { get; private set; }

        public BlackHandStats(BlackHandFacade blackHandFacade)
        {
            _blackHandFacade = blackHandFacade;
        }

        public void SubscribeToEvents(bool flag)
        {
            if (_blackHandFacade == null) return;

            if (flag)
            {
                _blackHandFacade.RotateToHero.OnFlipLeft += SetLoockLeft;
                _blackHandFacade.RotateToForward.OnFlipLeft += SetLoockLeft;
            }
            else
            {
                _blackHandFacade.RotateToHero.OnFlipLeft -= SetLoockLeft;
                _blackHandFacade.RotateToForward.OnFlipLeft -= SetLoockLeft;
            }
        }

        public void Block()
        {
            IsBlock = true;
        }

        public void UnBlock()
        {
            IsBlock = false;
        }

        public void SetLoockLeft(bool isLoockLeft)
        {
            IsLoockLeft = isLoockLeft;
        }
    }
}