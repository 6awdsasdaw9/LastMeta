using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Logic.Common;
using Code.Services.PauseListeners;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    public class BlackHandFacade : EnemyFacade 
    {
        public EnemyCollisionAttack CollisionAttack;
        public EnemyMelleAttack MelleAttack;
        public EnemySpikeRangeAttack SpikeAttack;
        public EnemyMovementPatrol Patrol;
        public RotateToHero RotateToHero;
        public AgentRotateToForfard RotateToForward;
        
        protected  override void InitComponents()
        {
            Stats = new BlackHandStats(this);
            CollisionAttack.Init(hero,data.CollisionAttackData,collisionAttackDamage);
            MelleAttack.Init(hero, data.MelleAttackData, Stats, Animator);
            SpikeAttack.Init(hero, Stats, data.SpikeAttackData, Animator);
            Patrol.Init(data.PatrolSpeed, data.PatrolCooldown, Stats);
            EnemyAudio.Init(data.AudioPath);
            Health.Set(data.HealthData);
        }
        
        private void OnEnable()
        {
            Stats.SubscribeToEvents(true);
        }

        private void OnDisable()
        {
            Stats.SubscribeToEvents(false);
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