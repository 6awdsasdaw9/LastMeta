using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services.PauseListeners;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    public class BlackHandFacade : EnemyFacade, IResumeListener
    {
        public EnemyMelleAttack MelleAttack;
        public EnemySpikeRangeAttack SpikeAttack;
        public EnemyMovementPatrol Patrol;
        public EnemyMovementToHero MovementToHero;
        
        [Inject]
        private void Construct(DiContainer container)
        {
            data = container.Resolve<EnemiesConfig>().GetByType(Type);
            hero = container.Resolve<IHero>();
            
            if (hero == null || data == null) return;

          container.Resolve<PauseListenerStorage>().Add(this);
          InitComponents();
        }

        private void InitComponents()
        {
            Stats = new BlackHandStats(this);
            
            MelleAttack.Init(hero, data.MelleAttackData, data.PushData, Stats, Animator);

            SpikeAttack.Init(hero,Stats,data.SpikeAttackData,Animator);
            
            
            Patrol.Init(data.PatrolSpeed, data.PatrolCooldown, Stats);
            MovementToHero.Init(hero.Transform, data.MoveSpeed);
            
            EnemyAudio.Init(data.AudioPath);
            Health.Set(data.HealthData);
        }

        public void OnPause()
        {
            Stats.Block();
            MovementToHero.OnPause();
            Patrol.OnPause();
        }

        public void OnResume()
        {
            Stats.UnBlock();
            MovementToHero.OnResume();
        }
    }
    
    public class BlackHandStats:IEnemyStats
    {
        private readonly BlackHandFacade _blackHandFacade;
        public bool IsPatroling => _blackHandFacade.Patrol.IsMoving;
        public bool IsMovingToHero => _blackHandFacade.MovementToHero.IsMoving;
        public bool IsAttacking => IsMelleAttacking || IsRangeAttacing;
        public bool IsMelleAttacking => _blackHandFacade.MelleAttack.IsAttacking;
        public bool IsRangeAttacing => false;
        public bool IsBlock { get; private set; }
        
        public BlackHandStats(BlackHandFacade blackHandFacade)
        {
            _blackHandFacade = blackHandFacade;
        }

        public void Block()
        {
            IsBlock = true;
        }

        public void UnBlock()
        {
            IsBlock = false;
        }
    }
}