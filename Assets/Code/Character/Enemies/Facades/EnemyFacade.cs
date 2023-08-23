using Code.Character.Enemies.EnemiesInterfaces;
using Code.Character.Hero.HeroInterfaces;
using Code.Data.Configs;
using Code.Services.PauseListeners;
using UnityEngine;
using Zenject;

namespace Code.Character.Enemies.EnemiesFacades
{
    [RequireComponent(typeof(EnemyHealth))]
    public abstract class EnemyFacade: MonoBehaviour, IResumeListener
    {
        public EnemyType Type;
        public IEnemyStats Stats;
        public EnemyHealth Health;
        public EnemyAudio EnemyAudio;
        public EnemyAnimator Animator;

        protected EnemyData data;
        protected  IHero hero;

        protected float collisionAttackDamage;

        [Inject]
        protected void Construct(DiContainer container)
        {
            hero = container.Resolve<IHero>();
            
            var config = container.Resolve<EnemiesConfig>();
            data = config.GetByType(Type);
            collisionAttackDamage = config.CollisionDamage;
            
            if (hero == null || data == null) return;

            container.Resolve<PauseListenerStorage>().Add(this);

            InitComponents();
        }

        protected  virtual void OnValidate()
        {
            EnemyAudio = GetComponentInChildren<EnemyAudio>();
            Health = GetComponent<EnemyHealth>();
            Animator = GetComponent<EnemyAnimator>();
        }
        protected abstract void InitComponents();
        public virtual void OnPause()
        {
        }

        public virtual void OnResume()
        {
        }
    }
}

