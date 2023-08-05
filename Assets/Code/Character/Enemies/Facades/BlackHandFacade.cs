using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services.PauseListeners;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    public class BlackHandFacade : Enemy, IResumeListener
    {
        public EnemyType Type;
        public IEnemyStats Stats;
        public EnemyMelleAttack MelleAttack;
        public EnemyMovementPatrol Patrol;
        public EnemyMovementToHero MovementToHero;
        public EnemyHealth Health;
        public EnemyAudio EnemyAudio;

        private EnemyData _data;
        private IHero _hero;

        [Inject]
        private void Construct(DiContainer container)
        {
            _data = container.Resolve<EnemiesConfig>().GetByType(Type);
            _hero = container.Resolve<IHero>();
            
            if (_hero == null || _data == null) return;

          container.Resolve<PauseListenerStorage>().Add(this);
            InitComponents();
        }

        private void InitComponents()
        {
            Stats = new BlackHandStats(this);
            MelleAttack.Init(_hero, _data.DamageParam, _data.PushData, Stats);
            Patrol.Init(_data.PatrolSpeed, _data.PatrolCooldown, Stats);
            MovementToHero.Init(_hero.Transform, _data.MoveSpeed);
            EnemyAudio.Init(_data.AudioPath);
            Health.Set(_data.HealthData);
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